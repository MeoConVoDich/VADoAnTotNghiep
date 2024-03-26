﻿using DoAnTotNghiep.Config;
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
        public static string ADMIN = "1";

        public static string STAFFPROFILE_VIEW = "2";
        public static string STAFFPROFILE_ADD = "3";
        public static string STAFFPROFILE_EDIT = "4";
        public static string STAFFPROFILE_DELETE = "5";

        public static string DISCIPLINE_BONUS_VIEW = "6";
        public static string DISCIPLINE_BONUS_ADD = "7";
        public static string DISCIPLINE_BONUS_EDIT = "8";
        public static string DISCIPLINE_BONUS_DELETE = "9";

        public static string SETUP_TIMEKEEPING = "10";

        public static string TIMEKEEPINGSHIFTSTAFF_VIEW = "11";
        public static string TIMEKEEPINGSHIFTSTAFF_EDIT = "13";
        public static string TIMEKEEPINGSHIFTSTAFF_DELETE = "14";

        public static string FINGERPRINT_VIEW = "15";
        public static string FINGERPRINT_ADD = "16";

        public static string TIMEKEEPINGAGGREGATE_VIEW = "17";
        public static string TIMEKEEPINGAGGREGATE = "19";

        public static string MANAGETIMEKEEPINGEXPLANATION_VIEW = "20";
        public static string MANAGETIMEKEEPINGEXPLANATION_ADD = "21";
        public static string MANAGETIMEKEEPINGEXPLANATION_EDIT = "22";
        public static string MANAGETIMEKEEPINGEXPLANATION_DELETE = "23";
        public static string MANAGETIMEKEEPINGEXPLANATION_APPROVE = "23A";

        public static string MANAGEVACATIONREGISTRATION_VIEW = "24";
        public static string MANAGEVACATIONREGISTRATION_ADD = "25";
        public static string MANAGEVACATIONREGISTRATION_EDIT = "26";
        public static string MANAGEVACATIONREGISTRATION_DELETE = "27";
        public static string MANAGEVACATIONREGISTRATION_APPROVE = "27A";

        public static string MANAGEOVERTIMEREGISTER_VIEW = "28";
        public static string MANAGEOVERTIMEREGISTER_ADD = "29";
        public static string MANAGEOVERTIMEREGISTER_EDIT = "30";
        public static string MANAGEOVERTIMEREGISTER_DELETE = "31";
        public static string MANAGEOVERTIMEREGISTER_APPROVE = "31A";

        public static string ROLE_VIEW = "32";
        public static string ROLE_ADD = "33";
        public static string ROLE_EDIT = "34";
        public static string ROLE_DELETE = "35";
        public static string ROLE_SETCLAIM = "36";

        public static string ACCOUNT_VIEW = "37";
        public static string ACCOUNT_ADD = "38";
        public static string ACCOUNT_DELETE = "40";
        public static string ACCOUNT_SETROLE = "41";
        public static string ACCOUNT_SETCLAIM = "42";
        public static string ACCOUNT_CHANGEPASSWORD = "43";

        public static string STAFFRELATIONSHIP_VIEW = "44";
        public static string STAFFRELATIONSHIP_ADD = "45";
        public static string STAFFRELATIONSHIP_EDIT = "46";
        public static string STAFFRELATIONSHIP_DELETE = "47";

        public static string OVERTIMEAGGREGATE_VIEW = "48";
        public static string OVERTIMEAGGREGATE = "49";
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

        public bool IsProfileMenuVisible()
        {
            return ADMIN || STAFFPROFILE_VIEW || DISCIPLINE_BONUS_VIEW || STAFFRELATIONSHIP_VIEW;
        }

        public bool IsSystemMenuVisible()
        {
            return ADMIN || STAFFPROFILE_VIEW || DISCIPLINE_BONUS_VIEW || STAFFRELATIONSHIP_VIEW;
        }

        public bool IsTimekeepingManagementMenuVisible()
        {
            return ADMIN || MANAGETIMEKEEPINGEXPLANATION_VIEW || MANAGEOVERTIMEREGISTER_VIEW || MANAGEVACATIONREGISTRATION_VIEW;
        }

        public bool IsTimekeepingMenuVisible()
        {
            return ADMIN || TIMEKEEPINGSHIFTSTAFF_VIEW || FINGERPRINT_VIEW 
                || MANAGETIMEKEEPINGEXPLANATION_VIEW || MANAGEOVERTIMEREGISTER_VIEW 
                || MANAGEVACATIONREGISTRATION_VIEW || OVERTIMEAGGREGATE_VIEW 
                || TIMEKEEPINGAGGREGATE_VIEW || SETUP_TIMEKEEPING;
        }

        public bool ADMIN;

        public bool STAFFPROFILE_VIEW;
        public bool STAFFPROFILE_ADD;
        public bool STAFFPROFILE_EDIT;
        public bool STAFFPROFILE_DELETE;

        public bool DISCIPLINE_BONUS_VIEW;
        public bool DISCIPLINE_BONUS_ADD;
        public bool DISCIPLINE_BONUS_EDIT;
        public bool DISCIPLINE_BONUS_DELETE;

        public bool SETUP_TIMEKEEPING;

        public bool TIMEKEEPINGSHIFTSTAFF_VIEW;
        public bool TIMEKEEPINGSHIFTSTAFF_EDIT;
        public bool TIMEKEEPINGSHIFTSTAFF_DELETE;

        public bool FINGERPRINT_VIEW;
        public bool FINGERPRINT_ADD;

        public bool TIMEKEEPINGAGGREGATE_VIEW;
        public bool TIMEKEEPINGAGGREGATE;

        public bool MANAGETIMEKEEPINGEXPLANATION_VIEW;
        public bool MANAGETIMEKEEPINGEXPLANATION_ADD;
        public bool MANAGETIMEKEEPINGEXPLANATION_EDIT;
        public bool MANAGETIMEKEEPINGEXPLANATION_DELETE;
        public bool MANAGETIMEKEEPINGEXPLANATION_APPROVE;

        public bool MANAGEVACATIONREGISTRATION_VIEW;
        public bool MANAGEVACATIONREGISTRATION_ADD;
        public bool MANAGEVACATIONREGISTRATION_EDIT;
        public bool MANAGEVACATIONREGISTRATION_DELETE;
        public bool MANAGEVACATIONREGISTRATION_APPROVE;

        public bool MANAGEOVERTIMEREGISTER_VIEW;
        public bool MANAGEOVERTIMEREGISTER_ADD;
        public bool MANAGEOVERTIMEREGISTER_EDIT;
        public bool MANAGEOVERTIMEREGISTER_DELETE;
        public bool MANAGEOVERTIMEREGISTER_APPROVE;

        public bool ROLE_VIEW;
        public bool ROLE_ADD;
        public bool ROLE_EDIT;
        public bool ROLE_DELETE;
        public bool ROLE_SETCLAIM;

        public bool ACCOUNT_VIEW;
        public bool ACCOUNT_ADD;
        public bool ACCOUNT_DELETE;
        public bool ACCOUNT_SETROLE;
        public bool ACCOUNT_SETCLAIM;
        public bool ACCOUNT_CHANGEPASSWORD;

        public bool STAFFRELATIONSHIP_VIEW;
        public bool STAFFRELATIONSHIP_ADD;
        public bool STAFFRELATIONSHIP_EDIT;
        public bool STAFFRELATIONSHIP_DELETE;

        public bool OVERTIMEAGGREGATE_VIEW;
        public bool OVERTIMEAGGREGATE;
    }
}
