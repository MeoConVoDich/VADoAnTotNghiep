using DoAnTotNghiep.Map;
using FluentNHibernate.MappingModel.ClassBased;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Configuration = NHibernate.Cfg.Configuration;


namespace DoAnTotNghiep.Config
{
    static class NHibernateConfig
    {

        public static ISessionFactory BuildSessionFactory()
        {
            var cfg = new Configuration();

            string connectionString = "Data Source=DESKTOP-F75INI6;Initial Catalog=VAHRM;Integrated Security=True";
            cfg.DataBaseIntegration(db =>
            {
                db.ConnectionString = connectionString;
                db.Dialect<MsSql2012Dialect>();
                db.Driver<SqlClientDriver>();
            });
            var mapper = new ModelMapper();
            mapper.AddMapping(typeof(UsersMap));
            mapper.AddMapping(typeof(BonusDisciplineMap));
            mapper.AddMapping(typeof(TimekeepingTypeMap));
            mapper.AddMapping(typeof(OvertimeRateMap));
            mapper.AddMapping(typeof(TimekeepingShiftMap));
            mapper.AddMapping(typeof(TimekeepingFormulaMap));
            mapper.AddMapping(typeof(OvertimeMap));
            mapper.AddMapping(typeof(VacationMap));
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(domainMapping);
            return cfg.BuildSessionFactory();
        }
    }
}
