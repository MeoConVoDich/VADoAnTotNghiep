using DoAnTotNghiep.Config;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Shared
{
    public static class CustomClaimTypes
    {
        public static string Permission = "p_";
        public static bool HasClaim(this List<string> claims, string value)
        {
            if (claims == null)
            {
                return false;
            }
            if (claims.Exists(c => c == "Admin"))
            {
                return true;
            }
            return claims.Any(c => c == value);
        }

        public static bool IsAdmin(this IEnumerable<Claim> claims) => claims.Any(c => c.Type == Permission && c.Value == PermissionKey.ADMIN);
    }

    public class PermissionKey
    {
        public static string ADMIN = "Admin";
        public static string ACCOUNT_VIEW = "1";
        public static string ACCOUNT_ADD = "2";
        public static string ACCOUNT_EDIT = "3";
        public static string ACCOUNT_DELETE = "4";
        public static string ACCOUNT_SETROLE = "5";
        public static string ACCOUNT_SETCLAIM = "6";
        public static string ACCOUNT_CHANGEPASSWORD = "7";

        public static string ROLE_VIEW = "8";
        public static string ROLE_ADD = "9";
        public static string ROLE_EDIT = "10";
        public static string ROLE_DELETE = "11";
        public static string ROLE_SETCLAIM = "12";

        public static string AUDITLOG_VIEW = "13";
        public static string AUDITLOG_ADD = "14";
        public static string AUDITLOG_EDIT = "15";
        public static string AUDITLOG_DELETE = "16";

        public static string CONTRACT_VIEW = "17";
        public static string CONTRACT_ADD = "18";
        public static string CONTRACT_EDIT = "19";
        public static string CONTRACT_DELETE = "20";

        public static string DECISIONINFO_VIEW = "21";
        public static string DECISIONINFO_ADD = "22";
        public static string DECISIONINFO_EDIT = "23";
        public static string DECISIONINFO_DELETE = "24";

        public static string BENEFIT_VIEW = "25";
        public static string BENEFIT_ADD = "26";
        public static string BENEFIT_EDIT = "27";
        public static string BENEFIT_DELETE = "28";

        public static string ALLOWANCE_VIEW = "29";
        public static string ALLOWANCE_ADD = "30";
        public static string ALLOWANCE_EDIT = "31";
        public static string ALLOWANCE_DELETE = "32";

        public static string INSURANCEVOLATILITY_VIEW = "33";
        public static string INSURANCEVOLATILITY_ADD = "34";
        public static string INSURANCEVOLATILITY_EDIT = "35";
        public static string INSURANCEVOLATILITY_DELETE = "36";

        public static string LEAVEJOB_VIEW = "37";
        public static string LEAVEJOB_ADD = "38";
        public static string LEAVEJOB_EDIT = "39";
        public static string LEAVEJOB_DELETE = "40";

        public static string REMINDERSETTING_VIEW = "41";
        public static string REMINDERSETTING_ADD = "42";

        public static string MANAGERREGISTERWORKOUTSIDE_VIEW = "43";
        public static string MANAGERREGISTERWORKOUTSIDE_ADD = "44";
        public static string MANAGERREGISTERWORKOUTSIDE_EDIT = "45";
        public static string MANAGERREGISTERWORKOUTSIDE_DELETE = "46";

        public static string APPROVETIMEKEEPINGEXPLANATION_VIEW = "47";
        public static string APPROVETIMEKEEPINGEXPLANATION_EDIT = "48";
        public static string APPROVETIMEKEEPINGEXPLANATION_EDITALL = "481";
        public static string APPROVETIMEKEEPINGEXPLANATION_DIRECTMANAGER = "482";

        public static string APPROVEWORKLATELEAVEEARLY_VIEW = "49";
        public static string APPROVEWORKLATELEAVEEARLY_EDIT = "50";
        public static string APPROVEWORKLATELEAVEEARLY_EDITALL = "501";
        public static string APPROVEWORKLATELEAVEEARLY_DIRECTMANAGER = "502";

        public static string MANAGEWORKLATELEAVEEARLY_VIEW = "51";
        public static string MANAGEWORKLATELEAVEEARLY_EDIT = "52";

        public static string MANAGETIMEKEEPINGEXPLANATION_VIEW = "53";
        public static string MANAGETIMEKEEPINGEXPLANATION_APPROVE = "531";

        public static string APPROVALREGISTERWORKOUTSIDE_VIEW = "54";
        public static string APPROVALREGISTERWORKOUTSIDE_EDIT = "55";
        public static string APPROVALREGISTERWORKOUTSIDE_EDITALL = "551";
        public static string APPROVALREGISTERWORKOUTSIDE_DIRECTMANAGER = "552";

        public static string DEPARTMENT_VIEW = "56";
        public static string DEPARTMENT_ADD = "57";
        public static string DEPARTMENT_EDIT = "58";
        public static string DEPARTMENT_DELETE = "59";

        public static string AUTOCONFIG_VIEW = "60";
        public static string AUTOCONFIG_ADD = "61";
        public static string AUTOCONFIG_EDIT = "62";
        public static string AUTOCONFIG_DELETE = "63";

        public static string CONCURRENTTITLE_VIEW = "64";
        public static string CONCURRENTTITLE_ADD = "65";
        public static string CONCURRENTTITLE_EDIT = "66";
        public static string CONCURRENTTITLE_DELETE = "67";

        public static string BANK_VIEW = "68";
        public static string BANK_ADD = "69";
        public static string BANK_EDIT = "70";
        public static string BANK_DELETE = "71";

        public static string STAFFPROFILE_VIEW = "72";
        public static string STAFFPROFILE_DEPARTMENT = "721";
        public static string STAFFPROFILE_DIRECTMANAGER = "722";
        public static string STAFFPROFILE_ADD = "73";
        public static string STAFFPROFILE_EDIT = "74";
        public static string STAFFPROFILE_DELETE = "75";

        public static string STAFFRELATIONSHIP_VIEW = "76";
        public static string STAFFRELATIONSHIP_ADD = "77";
        public static string STAFFRELATIONSHIP_EDIT = "78";
        public static string STAFFRELATIONSHIP_DELETE = "79";

        public static string BONUS_VIEW = "80";
        public static string BONUS_ADD = "81";
        public static string BONUS_EDIT = "82";
        public static string BONUS_DELETE = "83";

        public static string DISCIPLINE_VIEW = "84";
        public static string DISCIPLINE_ADD = "85";
        public static string DISCIPLINE_EDIT = "86";
        public static string DISCIPLINE_DELETE = "87";

        public static string TIMEKEEPINGSHIFTSTAFF_VIEW = "92";
        public static string TIMEKEEPINGSHIFTSTAFF_ADD = "93";
        public static string TIMEKEEPINGSHIFTSTAFF_EDIT = "94";
        public static string TIMEKEEPINGSHIFTSTAFF_DELETE = "95";

        public static string TIMEKEEPINGPRODUCTSTAFF_VIEW = "96";
        public static string TIMEKEEPINGPRODUCTSTAFF_ADD = "97";
        public static string TIMEKEEPINGPRODUCTSTAFF_EDIT = "98";
        public static string TIMEKEEPINGPRODUCTSTAFF_DELETE = "99";

        public static string PRODUCTSTAFFSYNTHESIS_VIEW = "A1";
        public static string PRODUCTSTAFFSYNTHESIS_ADD = "A2";
        public static string PRODUCTSTAFFSYNTHESIS_EDIT = "A3";
        public static string PRODUCTSTAFFSYNTHESIS_DELETE = "A4";

        public static string VACATIONDAY_VIEW = "A9";
        public static string VACATIONDAY_ADD = "AA";
        public static string VACATIONDAY_EDIT = "AB";
        public static string VACATIONDAY_DELETE = "AC";

        public static string HANDLINGVIOLATIONS_VIEW = "AD";
        public static string HANDLINGVIOLATIONS_ADD = "AE";
        public static string HANDLINGVIOLATIONS_EDIT = "AF";
        public static string HANDLINGVIOLATIONS_DELETE = "AG";

        public static string SALARYKEEPING_VIEW = "AH";
        public static string SALARYKEEPING_ADD = "AI";
        public static string SALARYKEEPING_EDIT = "AJ";
        public static string SALARYKEEPING_DELETE = "AK";

        public static string MANAGEVACATIONREGISTRATION_VIEW = "AL";
        public static string MANAGEVACATIONREGISTRATION_ADD = "AM";
        public static string MANAGEVACATIONREGISTRATION_EDIT = "AN";
        public static string MANAGEVACATIONREGISTRATION_DELETE = "AO";

        public static string APPROVALVACATIONREGISTRATION_VIEW = "AP";
        public static string APPROVALVACATIONREGISTRATION_EDIT = "AQ";
        public static string APPROVALVACATIONREGISTRATION_EDITALL = "AQ1";
        public static string APPROVALVACATIONREGISTRATION_DIRECTMANAGER = "AQ2";

        public static string MANAGEOVERTIMEREGISTER_VIEW = "AR";
        public static string MANAGEOVERTIMEREGISTER_ADD = "AS";
        public static string MANAGEOVERTIMEREGISTER_EDIT = "AT";
        public static string MANAGEOVERTIMEREGISTER_DELETE = "AU";

        public static string APPROVALOVERTIMEREGISTER_VIEW = "AV";
        public static string APPROVALOVERTIMEREGISTER_EDIT = "AW";
        public static string APPROVALOVERTIMEREGISTER_EDITALL = "AW1";
        public static string APPROVALOVERTIMEREGISTER_DIRECTMANAGER = "AW2";

        public static string SALARYSTAFFIMPORT_VIEW = "AX";
        public static string SALARYSTAFFIMPORT_ADD = "AY";
        public static string SALARYSTAFFIMPORT_EDIT = "AZ";
        public static string SALARYSTAFFIMPORT_DELETE = "B1";

        public static string SALARYSTAFFPHASE_VIEW = "B2";
        public static string SALARYSTAFFPHASE_ADD = "B3";
        public static string SALARYSTAFFPHASE_EDIT = "B4";
        public static string SALARYSTAFFPHASE_DELETE = "B5";

        public static string FINGERPRINT_VIEW = "B6";
        public static string FINGERPRINT_ADD = "B7";

        public static string TIMEKEEPINGAGGREGATE_VIEW = "BG";
        public static string TIMEKEEPINGAGGREGATE_DELETE = "BJ";
        public static string TIMEKEEPINGAGGREGATE_ALL = "BJ1";
        public static string TIMEKEEPINGAGGREGATE_DEPARTMENT = "BJ2";
        public static string TIMEKEEPINGAGGREGATE_DIRECTMANAGER = "BJ3";

        public static string APPROVEWORKOUTSIDE_VIEW = "BO";
        public static string APPROVEWORKOUTSIDE_EDIT = "BP";

        public static string SALARYSTAFFSUMMARY_VIEW = "BR";
        public static string SALARYSTAFFSUMMARY_ADD = "BS";
        public static string SALARYSTAFFSUMMARY_EDIT = "BT";

        public static string PERSONALVACATIONDAY_VIEW = "BU";
        public static string SETUP_VIEW = "BV";

        public static string APPROVALCHANGENIGHTSHIFT_VIEW = "BW";
        public static string APPROVALCHANGENIGHTSHIFT_EDIT = "BX";
        public static string APPROVALCHANGENIGHTSHIFT_EDITALL = "BY";

        public static string MANAGECHANGENIGHTSHIFT_VIEW = "BZ";
        public static string MANAGECHANGENIGHTSHIFT_APPROVE = "CA";

        public static string TIMEKEEPINGNIGHTSHIFT_VIEW = "CB";
        public static string TIMEKEEPINGNIGHTSHIFT_ADD = "CC";
        public static string TIMEKEEPINGNIGHTSHIFT_EDIT = "CD";
        public static string TIMEKEEPINGNIGHTSHIFT_DELETE = "CE";

        public static string NIGHTSHIFTGROUP_VIEW = "CF";
        public static string NIGHTSHIFTGROUP_ADD = "CG";
        public static string NIGHTSHIFTGROUP_EDIT = "CH";
        public static string NIGHTSHIFTGROUP_DELETE = "CI";

        public static string NIGHTSHIFTAGGREGATE_VIEW = "CJ";
        public static string NIGHTSHIFTAGGREGATE_ADD = "CK";
        public static string NIGHTSHIFTAGGREGATE_EDIT = "CL";
        public static string NIGHTSHIFTAGGREGATE_DELETE = "CM";

        public static string TIMEKEEPINGSHIFTGROUP_VIEW = "CN";
        public static string TIMEKEEPINGSHIFTGROUP_ADD = "CO";
        public static string TIMEKEEPINGSHIFTGROUP_EDIT = "CP";
        public static string TIMEKEEPINGSHIFTGROUP_DELETE = "CQ";

        public static string CHANGENIGHTSHIFTOTHER_VIEW = "CR";
        public static string CHANGENIGHTSHIFTOTHER_ADD = "CS";
        public static string CHANGENIGHTSHIFTOTHER_EDIT = "CT";
        public static string CHANGENIGHTSHIFTOTHER_DELETE = "CU";

        public static string REPORT_HRSYSTEM_VIEW = "D1";
        public static string REPORT_HRSYSTEM_ADD = "D2";
        public static string REPORT_HRSYSTEM_EDIT = "D3";
        public static string REPORT_HRSYSTEM_DELETE = "D4";

        public static string REPORT_PAYROLL_VIEW = "D5";
        public static string REPORT_PAYROLL_ADD = "D6";
        public static string REPORT_PAYROLL_EDIT = "D7";
        public static string REPORT_PAYROLL_DELETE = "D8";

        public static string MATERNITY_VIEW = "DA";
        public static string MATERNITY_ADD = "DB";
        public static string MATERNITY_EDIT = "DC";
        public static string MATERNITY_DELETE = "DD";

        public static string ARRANGESHIFTDEPARTMENT_VIEW = "DE";
        public static string NIGHTSHIFTSTAFFDEPARTMENT_VIEW = "DF";
        public static string SCANDEVICE_VIEW = "DG";

        public static string NIGHTSHIFTSETUP_VIEW = "DG1";

        public static string TIMEKEEPINGBYHAND_VIEW = "DH";
        public static string TIMEKEEPINGBYHAND_EDIT = "DJ";
        public static string TIMEKEEPINGBYHAND_EDITALL = "Dk";
        public static string TIMEKEEPINGBYHAND_DIRECTMANAGER = "Dk1";
        public static string TIMEKEEPINGBYHAND_DELETE = "DL";

        public static string APPROVALCHANGESHIFT_VIEW = "DM";
        public static string APPROVALCHANGESHIFT_EDIT = "DN";
        public static string APPROVALCHANGESHIFT_EDITALL = "DO";

        public static string MANAGECHANGESHIFT_VIEW = "DP";
        public static string MANAGECHANGESHIFT_ADD = "DQ";
        public static string MANAGECHANGESHIFT_EDIT = "DR";
        public static string MANAGECHANGESHIFT_DELETE = "DS";

        public static string PRODUCT_SALARY_VIEW = "DT";
        public static string PRODUCT_SALARY_ADD = "DU";
        public static string PRODUCT_SALARY_EDIT = "DV";
        public static string PRODUCT_SALARY_DELETE = "DW";

        public static string AUGMENTSTAFFPROFILE_VIEW = "DX";
        public static string ARRANGESHIFTFULL_VIEW = "FA";
        public static string NIGHTSHIFTSTAFFFULL_VIEW = "FB";

        public static string ARRANGESHIFTDIRECTMANAGER_VIEW = "FC";
        public static string ARRANGESHIFTGROUP_VIEW = "FD";

        public static string NIGHTSHIFTSTAFFDIRECTMANAGER_VIEW = "FE";
        public static string NIGHTSHIFTSTAFFGROUP_VIEW = "FF";

        public static string SETUPGROUPFULL_VIEW = "FG";
        public static string SETUPGROUPDEPARTMENT_VIEW = "FH";
        public static string SETUPGROUPDIRECTMANAGER_VIEW = "FI";
        public static string SETUPGROUP_VIEW = "FJ";

        public static string APPROVALCHANGENIGHTSHIFTDIRECTMANAGER_VIEW = "FK";
        public static string APPROVALCHANGENIGHTSHIFTGROUP_VIEW = "FL";

        public static string SETUP_COMPENSATORY_VIEW = "FM";
        public static string SETUP_COMPENSATORY_ADD = "FN";
        public static string SETUP_COMPENSATORY_EDIT = "FO";
        public static string SETUP_COMPENSATORY_DELETE = "FP";

        public static string COMPENSATORY_BYSHIFT_VIEW = "FQ";
        public static string COMPENSATORY_BYSHIFT_ADD = "FR";
        public static string COMPENSATORY_BYSHIFT_EDIT = "FS";
        public static string COMPENSATORY_BYSHIFT_DELETE = "FT";

        public static string COMPENSATORY_BYHOUR_VIEW = "FU";
        public static string COMPENSATORY_BYHOUR_ADD = "FV";
        public static string COMPENSATORY_BYHOUR_EDIT = "FW";
        public static string COMPENSATORY_BYHOUR_DELETE = "FX";

        public static string COMPENSATORY_AGGREGATE_VIEW = "FY";
        public static string COMPENSATORY_AGGREGATE_AGGREGATE = "FZ";

    }

    public class PermissionClaim
    {
        public void Claims(List<string> claims)
        {
            try
            {
                var fields = this.GetType().GetFields();
                foreach (var field in fields)
                {
                    field.SetValue(this, claims.HasClaim(typeof(PermissionKey).GetValueField(field.Name).ToString()));
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool IsContractMenuVisible()
        {
            return ADMIN || CONTRACT_VIEW;
        }

        public bool IsDeductMenuVisible()
        {
            return ADMIN || SETUP_VIEW || HANDLINGVIOLATIONS_VIEW || SALARYKEEPING_VIEW;
        }

        public bool IsSalaryMenuVisible()
        {
            return ADMIN || SETUP_VIEW || HANDLINGVIOLATIONS_VIEW || SALARYKEEPING_VIEW || SALARYSTAFFSUMMARY_VIEW || REPORT_PAYROLL_VIEW || PRODUCT_SALARY_VIEW;
        }

        public bool IsSystemMenuVisible()
        {
            return ADMIN || ACCOUNT_VIEW || ROLE_VIEW || DEPARTMENT_VIEW || SETUP_VIEW;
        }

        public bool IsBusinessProfileMenuVisible()
        {
            return ADMIN || MATERNITY_VIEW || STAFFRELATIONSHIP_VIEW
                || STAFFPROFILE_VIEW || STAFFPROFILE_DIRECTMANAGER || STAFFPROFILE_DEPARTMENT || DISCIPLINE_VIEW || BONUS_VIEW
                || DECISIONINFO_VIEW || BENEFIT_VIEW || ALLOWANCE_VIEW
                || SETUP_VIEW || LEAVEJOB_VIEW;
        }

        public bool IsProfileMenuVisible()
        {
            return ADMIN || MATERNITY_VIEW || STAFFRELATIONSHIP_VIEW || STAFFPROFILE_VIEW || STAFFPROFILE_DIRECTMANAGER || STAFFPROFILE_DEPARTMENT;
        }

        public bool IsDecisionInfoMenuVisible()
        {
            return ADMIN || DISCIPLINE_VIEW || BONUS_VIEW || DECISIONINFO_VIEW;
        }

        public bool IsAllowanceMenuVisible()
        {
            return ADMIN || BENEFIT_VIEW || ALLOWANCE_VIEW;
        }

        public bool IsTimekeepingApprovalMenuVisible()
        {
            return ADMIN || APPROVALVACATIONREGISTRATION_VIEW || APPROVEWORKLATELEAVEEARLY_VIEW || APPROVALOVERTIMEREGISTER_VIEW
                || APPROVALREGISTERWORKOUTSIDE_VIEW || APPROVETIMEKEEPINGEXPLANATION_VIEW || APPROVALCHANGENIGHTSHIFT_VIEW
                || ARRANGESHIFTDEPARTMENT_VIEW || NIGHTSHIFTSTAFFDEPARTMENT_VIEW || APPROVALCHANGESHIFT_VIEW
                || ARRANGESHIFTFULL_VIEW || ARRANGESHIFTDIRECTMANAGER_VIEW || ARRANGESHIFTGROUP_VIEW
                || NIGHTSHIFTSTAFFDEPARTMENT_VIEW || NIGHTSHIFTSTAFFFULL_VIEW || NIGHTSHIFTSTAFFDIRECTMANAGER_VIEW || NIGHTSHIFTSTAFFGROUP_VIEW;
        }

        public bool IsTimekeepingManagementDataMenuVisible()
        {
            return ADMIN || TIMEKEEPINGSHIFTSTAFF_VIEW || FINGERPRINT_VIEW;
        }

        public bool IsTimekeepingManagementMenuVisible()
        {
            return ADMIN || MANAGECHANGENIGHTSHIFT_VIEW || VACATIONDAY_VIEW
                || MANAGETIMEKEEPINGEXPLANATION_VIEW || MANAGERREGISTERWORKOUTSIDE_VIEW || MANAGEOVERTIMEREGISTER_VIEW
                || MANAGEWORKLATELEAVEEARLY_VIEW || MANAGEVACATIONREGISTRATION_VIEW || MANAGECHANGESHIFT_VIEW;
        }

        public bool IsTimekeepingMenuVisible()
        {
            return ADMIN || TIMEKEEPINGSHIFTSTAFF_VIEW || FINGERPRINT_VIEW || MANAGECHANGENIGHTSHIFT_VIEW || VACATIONDAY_VIEW
                || MANAGETIMEKEEPINGEXPLANATION_VIEW || MANAGERREGISTERWORKOUTSIDE_VIEW || MANAGEOVERTIMEREGISTER_VIEW
                || MANAGEWORKLATELEAVEEARLY_VIEW || MANAGEVACATIONREGISTRATION_VIEW || TIMEKEEPINGAGGREGATE_VIEW
                || TIMEKEEPINGNIGHTSHIFT_VIEW || TIMEKEEPINGSHIFTGROUP_VIEW || SETUP_VIEW || TIMEKEEPINGBYHAND_VIEW || MANAGECHANGESHIFT_VIEW;
        }

        public bool ADMIN;
        public bool ACCOUNT_VIEW;
        public bool ACCOUNT_ADD;
        public bool ACCOUNT_EDIT;
        public bool ACCOUNT_DELETE;
        public bool ACCOUNT_SETROLE;
        public bool ACCOUNT_SETCLAIM;
        public bool ACCOUNT_CHANGEPASSWORD;

        public bool ROLE_VIEW;
        public bool ROLE_ADD;
        public bool ROLE_EDIT;
        public bool ROLE_DELETE;
        public bool ROLE_SETCLAIM;

        public bool AUDITLOG_VIEW;
        public bool AUDITLOG_ADD;
        public bool AUDITLOG_EDIT;
        public bool AUDITLOG_DELETE;

        public bool CONTRACT_VIEW;
        public bool CONTRACT_ADD;
        public bool CONTRACT_EDIT;
        public bool CONTRACT_DELETE;

        public bool DECISIONINFO_VIEW;
        public bool DECISIONINFO_ADD;
        public bool DECISIONINFO_EDIT;
        public bool DECISIONINFO_DELETE;

        public bool BENEFIT_VIEW;
        public bool BENEFIT_ADD;
        public bool BENEFIT_EDIT;
        public bool BENEFIT_DELETE;

        public bool ALLOWANCE_VIEW;
        public bool ALLOWANCE_ADD;
        public bool ALLOWANCE_EDIT;
        public bool ALLOWANCE_DELETE;

        public bool LEAVEJOB_VIEW;
        public bool LEAVEJOB_ADD;
        public bool LEAVEJOB_EDIT;
        public bool LEAVEJOB_DELETE;

        public bool MATERNITY_VIEW;
        public bool MATERNITY_ADD;
        public bool MATERNITY_EDIT;
        public bool MATERNITY_DELETE;

        public bool INSURANCEVOLATILITY_VIEW;
        public bool INSURANCEVOLATILITY_ADD;
        public bool INSURANCEVOLATILITY_EDIT;
        public bool INSURANCEVOLATILITY_DELETE;

        public bool REMINDERSETTING_VIEW;
        public bool REMINDERSETTING_ADD;

        public bool MANAGERREGISTERWORKOUTSIDE_VIEW;
        public bool MANAGERREGISTERWORKOUTSIDE_ADD;
        public bool MANAGERREGISTERWORKOUTSIDE_EDIT;
        public bool MANAGERREGISTERWORKOUTSIDE_DELETE;

        public bool APPROVETIMEKEEPINGEXPLANATION_VIEW;
        public bool APPROVETIMEKEEPINGEXPLANATION_EDIT;
        public bool APPROVETIMEKEEPINGEXPLANATION_EDITALL;
        public bool APPROVETIMEKEEPINGEXPLANATION_DIRECTMANAGER;

        public bool APPROVEWORKLATELEAVEEARLY_VIEW;
        public bool APPROVEWORKLATELEAVEEARLY_EDIT;
        public bool APPROVEWORKLATELEAVEEARLY_EDITALL;
        public bool APPROVEWORKLATELEAVEEARLY_DIRECTMANAGER;

        public bool MANAGEWORKLATELEAVEEARLY_VIEW;
        public bool MANAGEWORKLATELEAVEEARLY_EDIT;

        public bool MANAGETIMEKEEPINGEXPLANATION_VIEW;
        public bool MANAGETIMEKEEPINGEXPLANATION_APPROVE;

        public bool APPROVALREGISTERWORKOUTSIDE_VIEW;
        public bool APPROVALREGISTERWORKOUTSIDE_EDIT;
        public bool APPROVALREGISTERWORKOUTSIDE_EDITALL;
        public bool APPROVALREGISTERWORKOUTSIDE_DIRECTMANAGER;

        public bool APPROVALCHANGENIGHTSHIFT_VIEW;
        public bool APPROVALCHANGENIGHTSHIFT_EDIT;
        public bool APPROVALCHANGENIGHTSHIFT_EDITALL;

        public bool MANAGECHANGENIGHTSHIFT_VIEW;
        public bool MANAGECHANGENIGHTSHIFT_APPROVE;

        public bool DEPARTMENT_VIEW;
        public bool DEPARTMENT_ADD;
        public bool DEPARTMENT_EDIT;
        public bool DEPARTMENT_DELETE;

        public bool AUTOCONFIG_VIEW;
        public bool AUTOCONFIG_ADD;
        public bool AUTOCONFIG_EDIT;
        public bool AUTOCONFIG_DELETE;

        public bool CONCURRENTTITLE_VIEW;
        public bool CONCURRENTTITLE_ADD;
        public bool CONCURRENTTITLE_EDIT;
        public bool CONCURRENTTITLE_DELETE;

        public bool BANK_VIEW;
        public bool BANK_ADD;
        public bool BANK_EDIT;
        public bool BANK_DELETE;

        public bool STAFFPROFILE_VIEW;
        public bool STAFFPROFILE_DIRECTMANAGER;
        public bool STAFFPROFILE_DEPARTMENT;
        public bool STAFFPROFILE_ADD;
        public bool STAFFPROFILE_EDIT;
        public bool STAFFPROFILE_DELETE;

        public bool STAFFRELATIONSHIP_VIEW;
        public bool STAFFRELATIONSHIP_ADD;
        public bool STAFFRELATIONSHIP_EDIT;
        public bool STAFFRELATIONSHIP_DELETE;

        public bool BONUS_VIEW;
        public bool BONUS_ADD;
        public bool BONUS_EDIT;
        public bool BONUS_DELETE;

        public bool DISCIPLINE_VIEW;
        public bool DISCIPLINE_ADD;
        public bool DISCIPLINE_EDIT;
        public bool DISCIPLINE_DELETE;

        public bool TIMEKEEPINGSHIFTSTAFF_VIEW;
        public bool TIMEKEEPINGSHIFTSTAFF_ADD;
        public bool TIMEKEEPINGSHIFTSTAFF_EDIT;
        public bool TIMEKEEPINGSHIFTSTAFF_DELETE;

        public bool TIMEKEEPINGPRODUCTSTAFF_VIEW;
        public bool TIMEKEEPINGPRODUCTSTAFF_ADD;
        public bool TIMEKEEPINGPRODUCTSTAFF_EDIT;
        public bool TIMEKEEPINGPRODUCTSTAFF_DELETE;

        public bool PRODUCTSTAFFSYNTHESIS_VIEW;
        public bool PRODUCTSTAFFSYNTHESIS_ADD;
        public bool PRODUCTSTAFFSYNTHESIS_EDIT;
        public bool PRODUCTSTAFFSYNTHESIS_DELETE;

        public bool VACATIONDAY_VIEW;
        public bool VACATIONDAY_ADD;
        public bool VACATIONDAY_EDIT;
        public bool VACATIONDAY_DELETE;

        public bool HANDLINGVIOLATIONS_VIEW;
        public bool HANDLINGVIOLATIONS_ADD;
        public bool HANDLINGVIOLATIONS_EDIT;
        public bool HANDLINGVIOLATIONS_DELETE;

        public bool SALARYKEEPING_VIEW;
        public bool SALARYKEEPING_ADD;
        public bool SALARYKEEPING_EDIT;
        public bool SALARYKEEPING_DELETE;

        public bool MANAGEVACATIONREGISTRATION_VIEW;
        public bool MANAGEVACATIONREGISTRATION_ADD;
        public bool MANAGEVACATIONREGISTRATION_EDIT;
        public bool MANAGEVACATIONREGISTRATION_DELETE;

        public bool APPROVALVACATIONREGISTRATION_VIEW;
        public bool APPROVALVACATIONREGISTRATION_EDIT;
        public bool APPROVALVACATIONREGISTRATION_EDITALL;
        public bool APPROVALVACATIONREGISTRATION_DIRECTMANAGER;


        public bool SALARYSTAFFIMPORT_VIEW;
        public bool SALARYSTAFFIMPORT_ADD;
        public bool SALARYSTAFFIMPORT_EDIT;
        public bool SALARYSTAFFIMPORT_DELETE;

        public bool APPROVALOVERTIMEREGISTER_VIEW;
        public bool APPROVALOVERTIMEREGISTER_EDIT;
        public bool APPROVALOVERTIMEREGISTER_EDITALL;
        public bool APPROVALOVERTIMEREGISTER_DIRECTMANAGER;

        public bool MANAGEOVERTIMEREGISTER_VIEW;
        public bool MANAGEOVERTIMEREGISTER_ADD;
        public bool MANAGEOVERTIMEREGISTER_EDIT;
        public bool MANAGEOVERTIMEREGISTER_DELETE;

        public bool SALARYSTAFFPHASE_VIEW;
        public bool SALARYSTAFFPHASE_ADD;
        public bool SALARYSTAFFPHASE_EDIT;
        public bool SALARYSTAFFPHASE_DELETE;

        public bool FINGERPRINT_VIEW;
        public bool FINGERPRINT_ADD;

        public bool TIMEKEEPINGAGGREGATE_VIEW;
        public bool TIMEKEEPINGAGGREGATE_DELETE;
        public bool TIMEKEEPINGAGGREGATE_ALL;
        public bool TIMEKEEPINGAGGREGATE_DEPARTMENT;
        public bool TIMEKEEPINGAGGREGATE_DIRECTMANAGER;

        public bool TIMEKEEPINGNIGHTSHIFT_VIEW;
        public bool TIMEKEEPINGNIGHTSHIFT_ADD;
        public bool TIMEKEEPINGNIGHTSHIFT_EDIT;
        public bool TIMEKEEPINGNIGHTSHIFT_DELETE;

        public bool NIGHTSHIFTGROUP_VIEW;
        public bool NIGHTSHIFTGROUP_ADD;
        public bool NIGHTSHIFTGROUP_EDIT;
        public bool NIGHTSHIFTGROUP_DELETE;

        public bool NIGHTSHIFTAGGREGATE_VIEW;
        public bool NIGHTSHIFTAGGREGATE_ADD;
        public bool NIGHTSHIFTAGGREGATE_EDIT;
        public bool NIGHTSHIFTAGGREGATE_DELETE;

        public bool SALARYSTAFFSUMMARY_VIEW;
        public bool SALARYSTAFFSUMMARY_ADD;
        public bool SALARYSTAFFSUMMARY_EDIT;

        public bool TIMEKEEPINGSHIFTGROUP_VIEW;
        public bool TIMEKEEPINGSHIFTGROUP_ADD;
        public bool TIMEKEEPINGSHIFTGROUP_EDIT;
        public bool TIMEKEEPINGSHIFTGROUP_DELETE;

        public bool CHANGENIGHTSHIFTOTHER_VIEW;
        public bool CHANGENIGHTSHIFTOTHER_ADD;
        public bool CHANGENIGHTSHIFTOTHER_EDIT;
        public bool CHANGENIGHTSHIFTOTHER_DELETE;

        public bool ARRANGESHIFTDEPARTMENT_VIEW;
        public bool ARRANGESHIFTFULL_VIEW;
        public bool NIGHTSHIFTSTAFFDEPARTMENT_VIEW;
        public bool NIGHTSHIFTSTAFFFULL_VIEW;

        public bool PERSONALVACATIONDAY_VIEW;

        public bool SETUP_VIEW;
        public bool SCANDEVICE_VIEW;

        public bool NIGHTSHIFTSETUP_VIEW;

        public bool TIMEKEEPINGBYHAND_VIEW;
        public bool TIMEKEEPINGBYHAND_EDIT;
        public bool TIMEKEEPINGBYHAND_EDITALL;
        public bool TIMEKEEPINGBYHAND_DELETE;
        public bool TIMEKEEPINGBYHAND_DIRECTMANAGER;

        public bool REPORT_HRSYSTEM_VIEW;
        public bool REPORT_HRSYSTEM_ADD;
        public bool REPORT_HRSYSTEM_EDIT;
        public bool REPORT_HRSYSTEM_DELETE;

        public bool REPORT_PAYROLL_VIEW;
        public bool REPORT_PAYROLL_ADD;
        public bool REPORT_PAYROLL_EDIT;
        public bool REPORT_PAYROLL_DELETE;

        public bool APPROVALCHANGESHIFT_VIEW;
        public bool APPROVALCHANGESHIFT_EDIT;
        public bool APPROVALCHANGESHIFT_EDITALL;

        public bool MANAGECHANGESHIFT_VIEW;
        public bool MANAGECHANGESHIFT_ADD;
        public bool MANAGECHANGESHIFT_EDIT;
        public bool MANAGECHANGESHIFT_DELETE;

        public bool PRODUCT_SALARY_VIEW;
        public bool PRODUCT_SALARY_ADD;
        public bool PRODUCT_SALARY_EDIT;
        public bool PRODUCT_SALARY_DELETE;

        public bool AUGMENTSTAFFPROFILE_VIEW;

        public bool ARRANGESHIFTDIRECTMANAGER_VIEW;
        public bool ARRANGESHIFTGROUP_VIEW;
        public bool NIGHTSHIFTSTAFFDIRECTMANAGER_VIEW;
        public bool NIGHTSHIFTSTAFFGROUP_VIEW;
        public bool SETUPGROUPFULL_VIEW;
        public bool SETUPGROUPDEPARTMENT_VIEW;
        public bool SETUPGROUPDIRECTMANAGER_VIEW;
        public bool SETUPGROUP_VIEW;

        public bool APPROVALCHANGENIGHTSHIFTDIRECTMANAGER_VIEW;
        public bool APPROVALCHANGENIGHTSHIFTGROUP_VIEW;

        public bool SETUP_COMPENSATORY_VIEW;
        public bool SETUP_COMPENSATORY_ADD;
        public bool SETUP_COMPENSATORY_EDIT;
        public bool SETUP_COMPENSATORY_DELETE;

        public bool COMPENSATORY_BYSHIFT_VIEW;
        public bool COMPENSATORY_BYSHIFT_ADD;
        public bool COMPENSATORY_BYSHIFT_EDIT;
        public bool COMPENSATORY_BYSHIFT_DELETE;

        public bool COMPENSATORY_BYHOUR_VIEW;
        public bool COMPENSATORY_BYHOUR_ADD;
        public bool COMPENSATORY_BYHOUR_EDIT;
        public bool COMPENSATORY_BYHOUR_DELETE;

        public bool COMPENSATORY_AGGREGATE_VIEW;
        public bool COMPENSATORY_AGGREGATE_AGGREGATE;
    }
}
