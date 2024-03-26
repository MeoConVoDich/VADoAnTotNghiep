﻿using AutoMapper;
using Blazored.SessionStorage;
using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.EditModel;
using DoAnTotNghiep.SearchModel;
using DoAnTotNghiep.Service;
using DoAnTotNghiep.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Timekeeping.Vacation
{
    public partial class VacationDetail : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] VacationService VacationService { get; set; }
        [Inject] UsersService UsersService { get; set; }
        [Inject] PermissionClaim PermissionClaim { get; set; }
        [Inject] ISessionStorageService sessionStorage { get; set; }
        [Parameter] public EventCallback Cancel { get; set; }
        [Parameter] public EventCallback ValueChanged { get; set; }
        [Parameter] public CreatorObject CreatorObject { get; set; }
        [Parameter] public List<TimekeepingType> TimekeepingTypes { get; set; }
        [Parameter] public EventCallback<Domain.Vacation> OpenApprovedModal { get; set; }
        [Parameter] public EventCallback<Domain.Vacation> OpenDeclinedModal { get; set; }
        [Parameter] public EventCallback<Domain.Vacation> OpenCancelApproveModal { get; set; }
        List<Users> StaffDatas = new List<Users>();
        List<Users> StaffSearchDatas = new List<Users>();
        VacationEditModel editModel = new VacationEditModel();
        InputWatcher inputWatcher;
        Domain.Vacation selectModel = new Domain.Vacation();
        bool addNew = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadStaffDataAsync();
                editModel.ReadOnly = true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void LoadEditModel(Domain.Vacation value, bool readOnly = false)
        {
            try
            {
                if (value.Id.IsNotNullOrEmpty())
                {
                    selectModel = value;
                }
                else
                {
                    selectModel = null;
                }
                editModel = Mapper.Map<VacationEditModel>(value);
                editModel.ReadOnly = readOnly;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task AddAsync()
        {
            try
            {
                editModel = new VacationEditModel();
                editModel.ApprovalStatus = ApprovalStatus.Pending;
                editModel.CreatorObject = CreatorObject.Staff;
                var usersId = await sessionStorage.GetItemAsync<string>("UsersId");
                if (usersId != null)
                {
                    var data = await UsersService.GetAllWithFilterAsync(new UsersSearch() { Id = usersId });
                    var user = data.Item1?.FirstOrDefault();
                    editModel.UsersData = Mapper.Map<UsersEditModel>(user);
                }
                else
                {
                    Notice.NotiWarning("Không tìm thấy tài khoản, yêu càu đăng nhập lại!");
                    return;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            

        }

        async Task SaveAsync()
        {
            try
            {
                bool result = new();
                var errorMessageStore = editModel.ValidateAll();
                if (!inputWatcher.Validate() || errorMessageStore.Any())
                {
                    if (errorMessageStore.Any())
                    {
                        inputWatcher.NotifyFieldChanged(errorMessageStore.First().Key, errorMessageStore);
                    }
                    Notice.NotiWarning("Dữ liệu còn thiếu hoặc không hợp lệ!");
                    return;
                }

                var existData = await VacationService.GetAllWithFilterAsync(new VacationSearch()
                {
                    CheckExist = true,
                    StartDateCheckExist = editModel.StartDate,
                    EndDateCheckExist = editModel.EndDate,
                    UsersId = editModel.UsersId
                });
                if (existData.Item1?.Any(c => c.Id != editModel.Id) == true)
                {
                    Notice.NotiWarning("Thời gian đăng ký đã tồn tại!");
                    return;
                }
                var vacation = Mapper.Map<Domain.Vacation>(editModel);
                if (vacation.Id.IsNotNullOrEmpty())
                {
                    result = await VacationService.UpdateAsync(vacation);
                }
                else
                {
                    vacation.Id = ObjectExtentions.GenerateGuid();
                    vacation.CreateDate = DateTime.Now;
                    result = await VacationService.AddAsync(vacation);
                }
                if (result)
                {
                    Notice.NotiSuccess("Cập nhật dữ liệu thành công!");
                    editModel.ReadOnly = true;
                    await ValueChanged.InvokeAsync();
                }
                else
                {
                    Notice.NotiError("Cập nhật dữ liệu thất bại!");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        async Task CancelAsync()
        {
            try
            {
                editModel = Mapper.Map<VacationEditModel>(selectModel);
                editModel.ReadOnly = true;
                await Cancel.InvokeAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void Edit()
        {
            try
            {
                editModel.ReadOnly = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task LoadStaffDataAsync()
        {
            try
            {
                var dataStaff = await UsersService.GetAllWithFilterAsync(new UsersSearch());
                StaffDatas = dataStaff.Item1 ?? new();
                StaffSearchDatas = StaffDatas.Take(5).ToList();
                if (editModel.UsersId.IsNotNullOrEmpty())
                {
                    var crStaff = StaffDatas.FirstOrDefault(c => c.Id == editModel.UsersId);
                    if (crStaff != null)
                    {
                        StaffSearchDatas.Add(crStaff);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void StaffChange(string staffId)
        {
            try
            {
                var selectedStaff = StaffDatas.FirstOrDefault(c => c.Id == staffId);
                if (selectedStaff != null)
                {
                    editModel.UsersData = Mapper.Map<UsersEditModel>(selectedStaff);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void OnStaffSearch(string e)
        {
            try
            {
                StaffSearchDatas.Clear();
                StaffSearchDatas = StaffDatas.Where(c => c.Name.Contains(e, StringComparison.OrdinalIgnoreCase)).ToList();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void ChooseBreakChange(string chooseBreak)
        {
            try
            {
                editModel.ChooseBreak = Enum.Parse<ChooseBreak>(chooseBreak);
                editModel.TimeCalculator();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
