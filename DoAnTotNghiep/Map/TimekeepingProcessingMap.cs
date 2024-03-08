using DoAnTotNghiep.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Map
{
    public class TimekeepingProcessingMap : ClassMapping<TimekeepingProcessing>
    {
        public TimekeepingProcessingMap()
        {
            Table("TimekeepingProcessing");

            Id(x => x.Id, map => map.Column("Id"));
            Property(x => x.Year);
            Property(x => x.Month);
            Property(x => x.UsersId);
            Property(x => x.Day01);
            Property(x => x.Day02);
            Property(x => x.Day03);
            Property(x => x.Day04);
            Property(x => x.Day05);
            Property(x => x.Day06);
            Property(x => x.Day07);
            Property(x => x.Day08);
            Property(x => x.Day09);
            Property(x => x.Day10);
            Property(x => x.Day11);
            Property(x => x.Day12);
            Property(x => x.Day13);
            Property(x => x.Day14);
            Property(x => x.Day15);
            Property(x => x.Day16);
            Property(x => x.Day17);
            Property(x => x.Day18);
            Property(x => x.Day19);
            Property(x => x.Day20);
            Property(x => x.Day21);
            Property(x => x.Day22);
            Property(x => x.Day23);
            Property(x => x.Day24);
            Property(x => x.Day25);
            Property(x => x.Day26);
            Property(x => x.Day27);
            Property(x => x.Day28);
            Property(x => x.Day29);
            Property(x => x.Day30);
            Property(x => x.Day31);
        }
    }
}
