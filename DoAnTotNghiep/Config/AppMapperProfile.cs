using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Config
{
    public class AppMappingProfile : AutoMapper.Profile
    {
        public static JsonSerializerOptions options = new JsonSerializerOptions();
        public static JsonSerializerOptions optionIgnoreNulls = new JsonSerializerOptions();
        public AppMappingProfile()
        {
            optionIgnoreNulls.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            #region Đăng ký các mapping giữa Data class và DB class
            var assembly = Assembly.GetAssembly(typeof(AppMappingProfile));
            var classes = assembly.ExportedTypes
               .Where(a => a.Namespace.Equals("DoAnTotNghiep.Domain"));
            foreach (Type type in classes)
            {
                CreateMap(type, type).ReverseMap();
                var dataClass = assembly.ExportedTypes.FirstOrDefault(c => c.Name == type.Name);
                if (dataClass != null)
                {
                    CreateMap(type, dataClass).ReverseMap();
                    var editModelClass = assembly.ExportedTypes.FirstOrDefault(c => c.Name == type.Name + "EditModel");
                    if (editModelClass != null)
                    {
                        CreateMap(dataClass, editModelClass).ReverseMap();
                        CreateMap(type, editModelClass).ReverseMap();
                    }
                    var viewModelClass = assembly.ExportedTypes.FirstOrDefault(c => c.Name == type.Name + "ViewModel");
                    if (viewModelClass != null)
                    {
                        CreateMap(viewModelClass, viewModelClass).ReverseMap();
                        CreateMap(dataClass, viewModelClass).ReverseMap();
                    }
                    var deleteModelClass = assembly.ExportedTypes.FirstOrDefault(c => c.Name == "Delete" + type.Name);
                    if (deleteModelClass != null)
                    {
                        CreateMap(type, deleteModelClass).ReverseMap();
                    }
                    var searchModelClass = assembly.ExportedTypes.FirstOrDefault(c => c.Name == type.Name + "Search");
                    var filterModelClass = assembly.ExportedTypes.FirstOrDefault(c => c.Name == type.Name + "FilterEditModel");
                    if (searchModelClass != null && filterModelClass != null)
                    {
                        CreateMap(searchModelClass, filterModelClass).ReverseMap();
                    }
                    var docxModelClass = assembly.ExportedTypes.FirstOrDefault(c => c.Name == type.Name + "DocxModel");
                    if (docxModelClass != null)
                    {
                        CreateMap(dataClass, docxModelClass).ReverseMap();
                        if (editModelClass != null)
                        {
                            CreateMap(editModelClass, docxModelClass).ReverseMap();
                        }
                    }
                    var excelModelClass = assembly.ExportedTypes.FirstOrDefault(c => c.Name == type.Name + "ExportExcel");
                    if (excelModelClass != null)
                    {
                        CreateMap(dataClass, excelModelClass).ReverseMap();
                        if (editModelClass != null)
                        {
                            CreateMap(editModelClass, excelModelClass).ReverseMap();
                        }
                    }
                }
            }
            #endregion
        }
    }
}
