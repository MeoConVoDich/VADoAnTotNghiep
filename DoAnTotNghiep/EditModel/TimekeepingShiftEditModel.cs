﻿using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class TimekeepingShiftEditModel : EditBaseModel
    {
        public virtual string Id { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Tên ca")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Mã ca")]
        public virtual string Code { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Thời gian bắt đầu")]
        public virtual DateTime? StartTime { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Thời gian kết thúc")]
        public virtual DateTime? EndTime { get; set; }

        [Display(Name = "Nghỉ giữa ca từ")]
        public virtual DateTime? StartBreaksTime { get; set; }

        [Display(Name = "Nghỉ giữa ca đến")]
        public virtual DateTime? EndBreaksTime { get; set; }

        [Display(Name = "Thời gian làm việc trong ca (giờ)")]
        public virtual decimal? Duration { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Kiểu công làm cả ngày")]
        public virtual string TimekeepingTypeFull { get; set; }

        [Display(Name = "Kiểu công chỉ làm trước nghỉ giữa ca")]
        public virtual string TimekeepingTypeFirst { get; set; }

        [Display(Name = "Kiểu công chỉ làm sau nghỉ giữa ca")]
        public virtual string TimekeepingTypeSecond { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Kiểu công không đi làm")]
        public virtual string TimekeepingTypeOff { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Trạng thái")]
        public virtual EffectiveState EffectiveState { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Kiểu nghỉ giữa ca")]
        public virtual BreaksTimeType BreaksTimeType { get; set; }
        public virtual DateTime CreateDate { get; set; }

        public Property<TimekeepingShiftEditModel> Property { get; set; } = new Property<TimekeepingShiftEditModel>();

        public TimekeepingShiftEditModel(bool isEdit = true)
        {
            DataSource[Property.NameProperty(c => c.EffectiveState)] = Enum.GetValues(typeof(EffectiveState)).Cast<EffectiveState>()
                 .Where(c => c != EffectiveState.All).OrderBy(c => c)
                .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            DataSource[Property.NameProperty(c => c.BreaksTimeType)] = Enum.GetValues(typeof(BreaksTimeType)).Cast<BreaksTimeType>()
               .Where(c => c != BreaksTimeType.All).OrderBy(c => c)
              .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            InputFields.Add<TimekeepingShiftEditModel, EffectiveState>(c => c.EffectiveState);
            InputFields.Add<TimekeepingShiftEditModel, BreaksTimeType>(c => c.BreaksTimeType);
            InputFields.Add<TimekeepingShiftEditModel>(c => c.StartTime);
            InputFields.Add<TimekeepingShiftEditModel>(c => c.EndTime);
            InputFields.Add<TimekeepingShiftEditModel>(c => c.StartBreaksTime);
            InputFields.Add<TimekeepingShiftEditModel>(c => c.EndBreaksTime);
            InputFields.Add<TimekeepingShiftEditModel>(c => c.TimekeepingTypeSecond);
            InputFields.Add<TimekeepingShiftEditModel>(c => c.TimekeepingTypeFirst);
        }

        public override Dictionary<string, List<string>> Validate(string nameProperty)
        {
            var Errors = new Dictionary<string, List<string>>();
            if (nameProperty == Property.NameProperty(c => c.EffectiveState))
            {
                if (EffectiveState == EffectiveState.All)
                {
                    Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                }
            }
            if (nameProperty == Property.NameProperty(c => c.BreaksTimeType))
            {
                if (BreaksTimeType == BreaksTimeType.All)
                {
                    Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                }
            }
            if (nameProperty == Property.NameProperty(c => c.StartBreaksTime))
            {
                if (BreaksTimeType == BreaksTimeType.Has)
                {
                    if (!StartBreaksTime.HasValue)
                    {
                        Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                    }
                    else
                    {
                        if (StartBreaksTime >= EndBreaksTime)
                        {
                            Errors.AddExist(nameProperty, "Thời gian bắt đầu nghỉ giữa ca phải lớn hơn thời gian kết thúc nghỉ giữa ca!");
                        }
                        if ((StartTime.HasValue && StartBreaksTime <= StartTime) || (EndTime.HasValue && StartBreaksTime >= EndTime))
                        {
                            Errors.AddExist(nameProperty, "Thời gian bắt đầu nghỉ giữa ca phải nằm trong thời gian làm việc!");
                        }
                    }
                }
            }
            if (nameProperty == Property.NameProperty(c => c.EndBreaksTime))
            {
                if (BreaksTimeType == BreaksTimeType.Has)
                {
                    if (!EndBreaksTime.HasValue)
                    {
                        Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                    }
                    else
                    {
                        if (StartBreaksTime >= EndBreaksTime)
                        {
                            Errors.AddExist(nameProperty, "Thời gian bắt đầu nghỉ giữa ca phải lớn hơn thời gian kết thúc nghỉ giữa ca!");
                        }
                        if ((StartTime.HasValue && EndBreaksTime <= StartTime) || (EndTime.HasValue && EndBreaksTime >= EndTime))
                        {
                            Errors.AddExist(nameProperty, "Thời gian kết thúc nghỉ giữa ca phải nằm trong thời gian làm việc!");
                        }
                    }
                }
            }
            if (nameProperty == Property.NameProperty(c => c.TimekeepingTypeSecond))
            {
                if (BreaksTimeType == BreaksTimeType.Has)
                {
                    if (TimekeepingTypeSecond.IsNullOrEmpty())
                    {
                        Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                    }
                }
            }
            if (nameProperty == Property.NameProperty(c => c.TimekeepingTypeFirst))
            {
                if (BreaksTimeType == BreaksTimeType.Has)
                {
                    if (TimekeepingTypeFirst.IsNullOrEmpty())
                    {
                        Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                    }
                }
            }
            if (nameProperty == Property.NameProperty(c => c.StartTime))
            {
                if (StartTime.HasValue && EndTime.HasValue && StartTime >= EndTime)
                {
                    Errors.AddExist(nameProperty, "Thời gian bắt đầu ca phải bé hơn thời gian kết thúc ca!");
                }
            }
            if (nameProperty == Property.NameProperty(c => c.EndTime))
            {
                if (StartTime.HasValue && EndTime.HasValue && StartTime >= EndTime)
                {
                    Errors.AddExist(nameProperty, "Thời gian kết thúc ca phải lơn hơn thời gian bắt đầu ca!");
                }
            }
            return Errors;
        }

        public void TimeCalculator()
        {
            try
            {
                if (StartTime.HasValue && EndTime.HasValue && EndTime > StartTime)
                {
                    var totalTime = EndTime - StartTime;
                    if (BreaksTimeType == BreaksTimeType.Has)
                    {
                        if (StartBreaksTime.HasValue && EndBreaksTime.HasValue
                        && EndBreaksTime > StartBreaksTime
                        && StartTime < StartBreaksTime && EndBreaksTime < EndTime)
                        {
                            var breakTime = EndBreaksTime - StartBreaksTime;
                            totalTime -= breakTime;
                        }
                        else
                        {
                            Duration = 0;
                            return;
                        }
                    }
                    Duration = (decimal?)Math.Round(totalTime.Value.TotalHours, 3);
                    return;
                }
                else
                {
                    Duration = 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
