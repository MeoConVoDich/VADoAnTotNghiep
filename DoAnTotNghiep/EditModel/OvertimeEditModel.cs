using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.EditModel
{
    public class OvertimeEditModel : EditBaseModel
    {
        public virtual string Id { get; set; }

        public virtual string UsersId { get => UsersData?.Id; set { } }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Tên nhân viên")]
        public virtual string StaffName { get => UsersData?.Name; set { } }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Mã nhân viên")]
        public virtual string StaffCode { get => UsersData?.Code; set { } }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Ngày làm thêm")]
        public virtual DateTime? RegisterDate { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Thời gian bắt đầu")]
        public virtual DateTime? StartTime { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Thời gian kết thúc")]
        public virtual DateTime? EndTime { get; set; }

        [Display(Name = "Nghỉ giữa ca từ")]
        public virtual DateTime? StartBreakTime { get; set; }

        [Display(Name = "Nghỉ giữa ca đến")]
        public virtual DateTime? EndBreakTime { get; set; }

        [Display(Name = "Tổng giờ làm")]
        public virtual decimal? TotalHour { get; set; }

        [Display(Name = "Số giờ làm ban ngày")]
        public virtual decimal? DayHourAmount { get; set; }

        [Display(Name = "Số giờ làm ban đêm")]
        public virtual decimal? NightHourAmount { get; set; }

        [Display(Name = "Lý do")]
        public virtual string Reason { get; set; }

        [Display(Name = "Lý do không phê duyệt")]
        public virtual string DisapprovedReason { get; set; }

        [Display(Name = "Trạng thái")]
        public virtual ApprovalStatus ApprovalStatus { get; set; }

        [Display(Name = "Ngày phê duyệt")]
        public virtual DateTime? ApprovedDate { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Kiểu nghỉ giữa ca")]
        public virtual BreaksTimeType BreaksTimeType { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc nhập!")]
        [Display(Name = "Hình thức")]
        public virtual OvertimeType OvertimeType { get; set; } = OvertimeType.NormalDay;
        public virtual DateTime CreateDate { get; set; }
        public virtual CreatorObject CreatorObject { get; set; }
        public virtual UsersEditModel UsersData { get; set; } = new UsersEditModel();

        public Property<OvertimeEditModel> Property { get; set; } = new Property<OvertimeEditModel>();

        public OvertimeEditModel(bool isEdit = true)
        {
            DataSource[Property.NameProperty(c => c.BreaksTimeType)] = Enum.GetValues(typeof(BreaksTimeType)).Cast<BreaksTimeType>()
              .Where(c => c != BreaksTimeType.All).OrderBy(c => c)
             .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            DataSource[Property.NameProperty(c => c.CreatorObject)] = Enum.GetValues(typeof(CreatorObject)).Cast<CreatorObject>()
             .Where(c => c != CreatorObject.All).OrderBy(c => c)
            .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            DataSource[Property.NameProperty(c => c.OvertimeType)] = Enum.GetValues(typeof(OvertimeType)).Cast<OvertimeType>()
            .Where(c => c != OvertimeType.All).OrderBy(c => c)
           .ToDictionary(c => c.ToString(), v => (ISelectItem)new SelectItem(v.ToString(), v.GetDescription()));
            InputFields.Add<OvertimeEditModel, BreaksTimeType>(c => c.BreaksTimeType);
            InputFields.Add<OvertimeEditModel, OvertimeType>(c => c.OvertimeType);
            InputFields.Add<OvertimeEditModel>(c => c.StartTime);
            InputFields.Add<OvertimeEditModel>(c => c.EndTime);
            InputFields.Add<OvertimeEditModel>(c => c.StartBreakTime);
            InputFields.Add<OvertimeEditModel>(c => c.EndBreakTime);
        }

        public override Dictionary<string, List<string>> Validate(string nameProperty)
        {
            var Errors = new Dictionary<string, List<string>>();
            if (nameProperty == Property.NameProperty(c => c.BreaksTimeType))
            {
                if (BreaksTimeType == BreaksTimeType.All)
                {
                    Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                }
            }
            if (nameProperty == Property.NameProperty(c => c.OvertimeType))
            {
                if (OvertimeType == OvertimeType.All)
                {
                    Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                }
            }
            if (nameProperty == Property.NameProperty(c => c.StartBreakTime))
            {
                if (BreaksTimeType == BreaksTimeType.Has)
                {
                    if (!StartBreakTime.HasValue)
                    {
                        Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                    }
                    else
                    {
                        if (StartBreakTime >= EndBreakTime)
                        {
                            Errors.AddExist(nameProperty, "Nghỉ giữa ca từ phải lớn hơn nghỉ giữa ca đến!");
                        }
                        if ((StartTime.HasValue && StartBreakTime <= StartTime) || (EndTime.HasValue && StartBreakTime >= EndTime))
                        {
                            Errors.AddExist(nameProperty, "Nghỉ giữa ca từ phải nằm trong thời gian làm việc!");
                        }
                    }
                }
            }
            if (nameProperty == Property.NameProperty(c => c.EndBreakTime))
            {
                if (BreaksTimeType == BreaksTimeType.Has)
                {
                    if (!EndBreakTime.HasValue)
                    {
                        Errors.AddExist(nameProperty, "Dữ liệu bắt buộc nhập!");
                    }
                    else
                    {
                        if (StartBreakTime >= EndBreakTime)
                        {
                            Errors.AddExist(nameProperty, "Nghỉ giữa ca từ phải lớn hơn nghỉ giữa ca đến!");
                        }
                        if ((StartTime.HasValue && EndBreakTime <= StartTime) || (EndTime.HasValue && EndBreakTime >= EndTime))
                        {
                            Errors.AddExist(nameProperty, "Nghỉ giữa ca đến phải nằm trong thời gian làm việc!");
                        }
                    }
                }
            }
            if (nameProperty == Property.NameProperty(c => c.StartTime))
            {
                if (StartTime.HasValue && EndTime.HasValue && StartTime >= EndTime)
                {
                    Errors.AddExist(nameProperty, "Thời gian bắt đầu phải bé hơn thời gian kết thúc!");
                }
            }
            if (nameProperty == Property.NameProperty(c => c.EndTime))
            {
                if (StartTime.HasValue && EndTime.HasValue && StartTime >= EndTime)
                {
                    Errors.AddExist(nameProperty, "Thời gian kết thúc phải lơn hơn thời gian bắt đầu!");
                }
            }
            return Errors;
        }

        public void TimeCalculator()
        {
            try
            {
                if (StartTime.HasValue)
                {
                    StartTime = DateTime.MinValue.AddHours(StartTime.Value.Hour).AddMinutes(StartTime.Value.Minute);
                }
                if (EndTime.HasValue)
                {
                    EndTime = DateTime.MinValue.AddHours(EndTime.Value.Hour).AddMinutes(EndTime.Value.Minute);
                }
                if (StartBreakTime.HasValue)
                {
                    StartBreakTime = DateTime.MinValue.AddHours(StartBreakTime.Value.Hour).AddMinutes(StartBreakTime.Value.Minute);
                }
                if (EndBreakTime.HasValue)
                {
                    EndBreakTime = DateTime.MinValue.AddHours(EndBreakTime.Value.Hour).AddMinutes(EndBreakTime.Value.Minute);
                }
                if (StartTime.HasValue && EndTime.HasValue && EndTime > StartTime)
                {
                    var totalTime = EndTime - StartTime;
                    DateTime hour6 = StartTime.Value.Date.AddHours(6);
                    DateTime hour22 = StartTime.Value.Date.AddHours(22);
                    TimeSpan? dayTime = TimeSpan.Zero;
                    TimeSpan? nightTime = TimeSpan.Zero;

                    if (EndTime < hour6 || StartTime > hour22)
                    {
                        nightTime = totalTime;
                    }
                    else if(StartTime < hour6)
                    {
                        nightTime = hour6 - StartTime;
                    }
                    else if(EndTime > hour22)
                    {
                        nightTime = EndTime - hour22;
                    }

                    if (BreaksTimeType == BreaksTimeType.Has)
                    {
                        if (StartBreakTime.HasValue && EndBreakTime.HasValue
                        && EndBreakTime > StartBreakTime
                        && StartTime < StartBreakTime && EndBreakTime < EndTime)
                        {
                            var breakTime = EndBreakTime - StartBreakTime;
                            totalTime -= breakTime;
                            if (EndBreakTime < hour6 || StartBreakTime > hour22)
                            {
                                nightTime -= breakTime;
                            }
                            else if (StartBreakTime < hour6)
                            {
                                nightTime -= hour6 - StartBreakTime;
                            }
                            else if (EndBreakTime > hour22)
                            {
                                nightTime -= EndBreakTime - hour22;
                            }
                        }
                        else
                        {
                            TotalHour = 0;
                            NightHourAmount = 0;
                            DayHourAmount = 0;
                            return;
                        }
                    }

                    TotalHour = (decimal?)Math.Round(totalTime.Value.TotalHours, 3);
                    NightHourAmount = (decimal?)Math.Round(nightTime.Value.TotalHours, 3);
                    DayHourAmount = TotalHour - NightHourAmount;
                }
                else
                {
                    TotalHour = 0;
                    NightHourAmount = 0;
                    DayHourAmount = 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
