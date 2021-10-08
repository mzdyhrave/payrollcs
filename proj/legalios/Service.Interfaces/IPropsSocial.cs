﻿using System;
using HraveMzdy.Legalios.Interfaces;

namespace HraveMzdy.Legalios.Service.Interfaces
{
    public interface IPropsSocial : IProps
    {
        Int32 MaxAnnualsBasis { get; }
        decimal FactorEmployer { get; }
        decimal FactorEmployerHigher { get; }
        decimal FactorEmployee { get; }
        decimal FactorEmployeeGarant { get; }
        decimal FactorEmployeeReduce { get; }
        Int32 MarginIncomeEmp { get; }
        Int32 MarginIncomeAgr { get; }
    }
}
