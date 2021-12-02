﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;
using HraveMzdy.Legalios.Service;
using HraveMzdy.Legalios.Service.Types;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Procezor.Service;
using HraveMzdy.Procezor.Payrolex.Registry.Constants;
using HraveMzdy.Procezor.Payrolex.Service;
using Procezor.PayrolexTest.Examples;

namespace Procezor.PayrolexTest.Service
{
    public record TestSpecParams(Int32 id, string name, string number, Int32 schedWeek, Int32 nonAtten, SpecGeneratorItem gen, ExampleParam exp);
    public class ServiceTestExamples2013
    {
        private readonly ITestOutputHelper output;

        private readonly IServiceProcezor _sut;
        private readonly IServiceLegalios _leg;

        static readonly bool yes = true;
        static readonly bool no = false;
        //Employment with Tax Advance, Withholding tax, no Minimum Health, Absence hours
        private static SpecGeneratorItem pomGenItem = new SpecGeneratorItem()
        {
            contractType = WorkContractTerms.WORKTERM_EMPLOYMENT_1,
            scheduleWeek = SpecGeneratorItem.DefScheduleWeek,
            salaryBasis = SpecGeneratorItem.DefSalaryBasis,
            agreemBasis = SpecGeneratorItem.DefAgreemBasis,
            socialPayer = SpecGeneratorItem.DefSocialPayer,
            healthPayer = SpecGeneratorItem.DefHealthPayer,
            healthMinim = SpecGeneratorItem.DefHealthMinim,
            socialEmper = SpecGeneratorItem.DefSocialEmper,
            healthEmper = SpecGeneratorItem.DefHealthEmper,
            penzisPayer = SpecGeneratorItem.DefPenzisPayer,
            taxingPayer = SpecGeneratorItem.DefTaxingPayer,
            taxDeclarat = SpecGeneratorItem.DefTaxDeclarat,
            taxBenPayer = SpecGeneratorItem.DefTaxBenPayer,
            taxBenDis01 = SpecGeneratorItem.DefTaxBenDis01,
            taxBenDis02 = SpecGeneratorItem.DefTaxBenDis02,
            taxBenDis03 = SpecGeneratorItem.DefTaxBenDis03,
            taxBebStudy = SpecGeneratorItem.DefTaxBebStudy,
            taxChildren = SpecGeneratorItem.DefTaxChildren,
        };
        //Employment - short-term contract with agreement to perform work
        private static SpecGeneratorItem dpcGenItem = new SpecGeneratorItem()
        {
            contractType = WorkContractTerms.WORKTERM_CONTRACTER_A,
            scheduleWeek = SpecGeneratorItem.DefScheduleWeek,
            salaryBasis = SpecGeneratorItem.DefSalaryBasis,
            agreemBasis = SpecGeneratorItem.DefAgreemBasis,
            socialPayer = SpecGeneratorItem.DefSocialPayer,
            healthPayer = SpecGeneratorItem.DefHealthPayer,
            healthMinim = SpecGeneratorItem.DefHealthMinim,
            socialEmper = SpecGeneratorItem.DefSocialEmper,
            healthEmper = SpecGeneratorItem.DefHealthEmper,
            penzisPayer = SpecGeneratorItem.DefPenzisPayer,
            taxingPayer = SpecGeneratorItem.DefTaxingPayer,
            taxDeclarat = SpecGeneratorItem.DefTaxDeclarat,
            taxBenPayer = SpecGeneratorItem.DefTaxBenPayer,
            taxBenDis01 = SpecGeneratorItem.DefTaxBenDis01,
            taxBenDis02 = SpecGeneratorItem.DefTaxBenDis02,
            taxBenDis03 = SpecGeneratorItem.DefTaxBenDis03,
            taxBebStudy = SpecGeneratorItem.DefTaxBebStudy,
            taxChildren = SpecGeneratorItem.DefTaxChildren,
        };
        //Employment - short-term contract with agreement to complete a job
        private static SpecGeneratorItem dppGenItem = new SpecGeneratorItem()
        {
            contractType = WorkContractTerms.WORKTERM_CONTRACTER_T,
            scheduleWeek = SpecGeneratorItem.DefScheduleWeek,
            salaryBasis = SpecGeneratorItem.DefSalaryBasis,
            agreemBasis = SpecGeneratorItem.DefAgreemBasis,
            socialPayer = SpecGeneratorItem.DefSocialPayer,
            healthPayer = SpecGeneratorItem.DefHealthPayer,
            healthMinim = SpecGeneratorItem.DefHealthMinim,
            socialEmper = SpecGeneratorItem.DefSocialEmper,
            healthEmper = SpecGeneratorItem.DefHealthEmper,
            penzisPayer = SpecGeneratorItem.DefPenzisPayer,
            taxingPayer = SpecGeneratorItem.DefTaxingPayer,
            taxDeclarat = SpecGeneratorItem.DefTaxDeclarat,
            taxBenPayer = SpecGeneratorItem.DefTaxBenPayer,
            taxBenDis01 = SpecGeneratorItem.DefTaxBenDis01,
            taxBenDis02 = SpecGeneratorItem.DefTaxBenDis02,
            taxBenDis03 = SpecGeneratorItem.DefTaxBenDis03,
            taxBebStudy = SpecGeneratorItem.DefTaxBebStudy,
            taxChildren = SpecGeneratorItem.DefTaxChildren,
        };

        private static ExampleParam exDefaults = new ExampleParam();
        private static ExampleParam exSrazNep0 = new ExampleParam()
        {
            srazDan = true,
            srazDanLimit = 0,
        };
        private static ExampleParam exSrazNep1 = new ExampleParam()
        {
            srazDan = true,
            srazDanLimit = 1,
        };
        private static ExampleParam exSrazNepPrev0 = new ExampleParam()
        {
            srazDanPrev = true,
            srazDanLimit = 0,
        };
        private static ExampleParam exSrazNepPrev1 = new ExampleParam()
        {
            srazDanPrev = true,
            srazDanLimit = 1,
        };
        private static ExampleParam exSalary(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryGen = true,
                salaryGenKc = kc,
            };
        }
        private static ExampleParam exAgreem(Int32 kc)
        {
            return new ExampleParam()
            {
                agreemGen = true,
                agreemGenKc = kc,
            };
        }
        private static ExampleParam exNoMinSalary(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryGen = true,
                salaryGenKc = kc,
                conMinZdr = true,
                conMinZdrBen = false,
            };
        }
        private static ExampleParam exNoMinAgreem(Int32 kc)
        {
            return new ExampleParam()
            {
                agreemGen = true,
                agreemGenKc = kc,
                conMinZdr = true,
                conMinZdrBen = false,
            };
        }
        private static ExampleParam exSalaryDite(Int32 kc, Int32 ditePoc1, Int32 ditePoc2, Int32 ditePoc3)
        {
            return new ExampleParam()
            {
                salaryGen = true,
                salaryGenKc = kc,
                taxChild = true,
                taxChildPor1 = ditePoc1,
                taxChildPor2 = ditePoc2,
                taxChildPor3 = ditePoc3,
                taxChildNorm = true,
            };
        }
        private static ExampleParam exDite(Int32 ditePoc1, Int32 ditePoc2, Int32 ditePoc3)
        {
            return new ExampleParam()
            {
                taxChild = true,
                taxChildPor1 = ditePoc1,
                taxChildPor2 = ditePoc2,
                taxChildPor3 = ditePoc3,
                taxChildNorm = true,
            };
        }
        private static ExampleParam exDiteZtp(Int32 ditePoc1, Int32 ditePoc2, Int32 ditePoc3)
        {
            return new ExampleParam()
            {
                taxChild = true,
                taxChildPor1 = ditePoc1,
                taxChildPor2 = ditePoc2,
                taxChildPor3 = ditePoc3,
                taxChildZtpp = true,
            };
        }
        private static ExampleParam exSalaryDiteZtp(Int32 kc, Int32 ditePoc1, Int32 ditePoc2, Int32 ditePoc3)
        {
            return new ExampleParam()
            {
                salaryGen = true,
                salaryGenKc = kc,
                taxChild = true,
                taxChildPor1 = ditePoc1,
                taxChildPor2 = ditePoc2,
                taxChildPor3 = ditePoc3,
                taxChildZtpp = true,
            };
        }
        private static ExampleParam exDiteMaxBonus = new ExampleParam()
        {
            salaryMaxBon = true,
            taxChild = true,
            taxChildPor1 = 1,
            taxChildPor2 = 1,
            taxChildPor3 = 5,
            taxChildZtpp = true,
        };
        private static ExampleParam exSalaryMinZdr(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryMinZdr = true,
                salaryMinZdrKc = kc,
            };
        }
        private static ExampleParam exSalaryMinZdrPrev(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryMinZdrPrev = true,
                salaryMinZdrKc = kc,
            };
        }
        private static ExampleParam exSalaryMaxZdr(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryMaxZdr = true,
                salaryMaxZdrKc = kc,
            };
        }
        private static ExampleParam exSalaryMaxZdrPrev(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryMaxZdrPrev = true,
                salaryMaxZdrKc = kc,
            };
        }
        private static ExampleParam exSalaryMaxSoc(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryMaxSoc = true,
                salaryMaxSocKc = kc,
            };
        }
        private static ExampleParam exSalaryMaxSocPrev(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryMaxSocPrev = true,
                salaryMaxSocKc = kc,
            };
        }
        private static ExampleParam exSalarySolTax(Int32 kc)
        {
            return new ExampleParam()
            {
                salarySolTax = true,
                salarySolTaxKc = kc,
            };
        }
        private static ExampleParam exSalarySolTaxPrev(Int32 kc)
        {
            return new ExampleParam()
            {
                salarySolTaxPrev = true,
                salarySolTaxKc = kc,
            };
        }
        private static ExampleParam exSalaryInv(Int32 kc, bool inv1, bool inv2, bool inv3)
        {
            return new ExampleParam()
            {
                salaryGen = true,
                salaryGenKc = kc,
                taxDisab = true,
                taxDisabBen1 = inv1,
                taxDisabBen2 = inv2,
                taxDisabBen3 = inv3,
            };
        }
        private static ExampleParam exTaxInval(bool inv1, bool inv2, bool inv3)
        {
            return new ExampleParam()
            {
                taxDisab = true,
                taxDisabBen1 = inv1,
                taxDisabBen2 = inv2,
                taxDisabBen3 = inv3,
            };
        }
        private static ExampleParam exTaxStudy = new ExampleParam()
        {
            taxStudy = true,
            taxStudyBen = true,
        };
        private static ExampleParam exSalaryUcastNem(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryNemUcast = true,
                salaryNemUcastKc = kc,
                podepTax = true,
                podepTaxVal = false,
                conMinZdr = true,
                conMinZdrBen = false,
            };
        }
        private static ExampleParam exSalaryUcastNemPrev(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryNemUcastPrev = true,
                salaryNemUcastKc = kc,
                podepTax = true,
                podepTaxVal = false,
                conMinZdr = true,
                conMinZdrBen = false,
            };
        }
        private static ExampleParam exSalaryUcastZdr(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryZdrUcast = true,
                salaryZdrUcastKc = kc,
                podepTax = true,
                podepTaxVal = false,
                conMinZdr = true,
                conMinZdrBen = false,
            };
        }
        private static ExampleParam exSalaryUcastZdrPrev(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryZdrUcastPrev = true,
                salaryZdrUcastKc = kc,
                podepTax = true,
                podepTaxVal = false,
                conMinZdr = true,
                conMinZdrBen = false,
            };
        }
        private static ExampleParam exSalaryUcastZdrEmp(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryZdrUcastEmp = true,
                salaryZdrUcastKc = kc,
                podepTax = true,
                podepTaxVal = false,
                conMinZdr = true,
                conMinZdrBen = false,
            };
        }
        private static ExampleParam exSalaryUcastZdrEmpPrev(Int32 kc)
        {
            return new ExampleParam()
            {
                salaryZdrUcastEmpPrev = true,
                salaryZdrUcastKc = kc,
                podepTax = true,
                podepTaxVal = false,
                conMinZdr = true,
                conMinZdrBen = false,
            };
        }

        private static readonly TestSpecParams[] _tests = new TestSpecParams[] {
                new TestSpecParams(101, "PP-Mzda_DanPoj-SlevyZaklad",      "101", 40, 0, pomGenItem, exDefaults), //, CZK 15000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(102, "PP-Mzda_DanPoj-SlevyDite1",       "102", 40, 0, pomGenItem, exSalaryDiteZtp(15600, 1, 0, 0)), //, CZK 15600    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 1,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(103, "PP-Mzda_DanPoj-BonusDite1",       "103", 40, 0, pomGenItem, exDiteZtp(1,0,0)), //, CZK 15000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 1,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(104, "PP-Mzda_DanPoj-BonusDite2",       "104", 40, 0, pomGenItem, exDiteZtp(1,1,0)), //, CZK 15000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 2,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(105, "PP-Mzda_DanPoj-MaxBonus",         "105", 40, 0, pomGenItem, exDiteMaxBonus), //, CZK 10000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 7,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(106, "PP-Mzda_DanPoj-MinZdravPrev",     "106", 40, 0, pomGenItem, exSalaryMinZdrPrev(-200)), //, CZK 7800     ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(107, "PP-Mzda_DanPoj-MinZdravCurr",     "107", 40, 0, pomGenItem, exSalaryMinZdr(-200)), //, CZK 7800     ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(108, "PP-Mzda_DanPoj-MaxZdravPrev",     "108", 40, 0, pomGenItem, exSalaryMaxZdrPrev(100)), //, CZK 1809964  ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(109, "PP-Mzda_DanPoj-MaxZdravCurr",     "109", 40, 0, pomGenItem, exSalaryMaxZdr(100)), //, CZK 1809964  ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(110, "PP-Mzda_DanPoj-MaxSocialPrev",    "110", 40, 0, pomGenItem, exSalaryMaxSocPrev(100)), //, CZK 1206676  ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(111, "PP-Mzda_DanPoj-MaxSocialCurr",    "111", 40, 0, pomGenItem, exSalaryMaxSoc(100)), //, CZK 1242532  ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(112, "PP-Mzda_DanPoj-SolidarDanPrev",   "112", 40, 0, pomGenItem, exSalarySolTaxPrev(1000)), //, CZK 104536   ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(113, "PP-Mzda_DanPoj-SolidarDanCurr",   "113", 40, 0, pomGenItem, exSalarySolTax(1000)), //, CZK 104536   ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(114, "PP-Mzda_DanPoj-DuchSpor",         "114", 40, 0, pomGenItem, exDefaults), //, CZK 15000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(115, "PP-Mzda_DanPoj-SlevyInv1",        "115", 40, 0, pomGenItem, exSalaryInv(20000, yes, no, no)), //, CZK 20000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  YES, NO, NO              ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(116, "PP-Mzda_DanPoj-SlevyInv2",        "116", 40, 0, pomGenItem, exTaxInval(no, yes, no)), //, CZK 15000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, YES, NO              ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(117, "PP-Mzda_DanPoj-SlevyInv3",        "117", 40, 0, pomGenItem, exTaxInval(no, no, yes)), //, CZK 15000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, YES, NO              ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(118, "PP-Mzda_DanPoj-SlevyStud",        "118", 40, 0, pomGenItem, exTaxStudy), //, CZK 15000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  YES                  ,  YES             ,  YES             , 
                new TestSpecParams(119, "PP-Mzda_DanPoj-SlevyZakl015",     "119", 40, 0, pomGenItem, exDefaults), //, CZK 15000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(120, "PP-Mzda_DanPoj-SlevyZakl020",     "120", 40, 0, pomGenItem, exSalary(20000 )), //, CZK 20000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(121, "PP-Mzda_DanPoj-SlevyZakl025",     "121", 40, 0, pomGenItem, exSalary(25000 )), //, CZK 25000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(122, "PP-Mzda_DanPoj-SlevyZakl030",     "122", 40, 0, pomGenItem, exSalary(30000 )), //, CZK 30000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(123, "PP-Mzda_DanPoj-SlevyZakl035",     "123", 40, 0, pomGenItem, exSalary(35000 )), //, CZK 35000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(124, "PP-Mzda_DanPoj-SlevyZakl040",     "124", 40, 0, pomGenItem, exSalary(40000 )), //, CZK 40000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(125, "PP-Mzda_DanPoj-SlevyZakl045",     "125", 40, 0, pomGenItem, exSalary(45000 )), //, CZK 45000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(126, "PP-Mzda_DanPoj-SlevyZakl050",     "126", 40, 0, pomGenItem, exSalary(50000 )), //, CZK 50000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(127, "PP-Mzda_DanPoj-SlevyZakl055",     "127", 40, 0, pomGenItem, exSalary(55000 )), //, CZK 55000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(128, "PP-Mzda_DanPoj-SlevyZakl060",     "128", 40, 0, pomGenItem, exSalary(60000 )), //, CZK 60000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(129, "PP-Mzda_DanPoj-SlevyZakl065",     "129", 40, 0, pomGenItem, exSalary(65000 )), //, CZK 65000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(130, "PP-Mzda_DanPoj-SlevyZakl070",     "130", 40, 0, pomGenItem, exSalary(70000 )), //, CZK 70000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(131, "PP-Mzda_DanPoj-SlevyZakl075",     "131", 40, 0, pomGenItem, exSalary(75000 )), //, CZK 75000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(132, "PP-Mzda_DanPoj-SlevyZakl080",     "132", 40, 0, pomGenItem, exSalary(80000 )), //, CZK 80000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(133, "PP-Mzda_DanPoj-SlevyZakl085",     "133", 40, 0, pomGenItem, exSalary(85000 )), //, CZK 85000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(134, "PP-Mzda_DanPoj-SlevyZakl090",     "134", 40, 0, pomGenItem, exSalary(90000 )), //, CZK 90000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(135, "PP-Mzda_DanPoj-SlevyZakl095",     "135", 40, 0, pomGenItem, exSalary(95000 )), //, CZK 95000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(136, "PP-Mzda_DanPoj-SlevyZakl100",     "136", 40, 0, pomGenItem, exSalary(100000)), //, CZK 100000   ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(137, "PP-Mzda_DanPoj-SlevyZakl105",     "137", 40, 0, pomGenItem, exSalary(105000)), //, CZK 105000   ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(138, "PP-Mzda_DanPoj-SlevyZakl110",     "138", 40, 0, pomGenItem, exSalary(110000)), //, CZK 110000   ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 

                new TestSpecParams(201, "PP-Mzda_NepodPoj-PrevLo",         "201", 40, 0, pomGenItem, exSrazNepPrev0), //, CZK 5000     ,  YES       , NO,  YES          ,  YES          ,  YES          ,  NO            ,  NO                , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(202, "PP-Mzda_NepodPoj-PrevHi",         "202", 40, 0, pomGenItem, exSrazNepPrev1), //, CZK 5001     ,  YES       , NO,  YES          ,  YES          ,  YES          ,  NO            ,  NO                , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(203, "PP-Mzda_NepodPoj-CurrLo",         "203", 40, 0, pomGenItem, exSrazNep0), //, CZK 5000     ,  YES       , NO,  YES          ,  YES          ,  YES          ,  NO            ,  NO                , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(204, "PP-Mzda_NepodPoj-CurrHi",         "204", 40, 0, pomGenItem, exSrazNep1), //, CZK 5001     ,  YES       , NO,  YES          ,  YES          ,  YES          ,  NO            ,  NO                , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 

                new TestSpecParams(301, "PP-Mzda_DanPoj-Dan099",           "301", 40, 0, pomGenItem, exNoMinAgreem(74)), //, CZK 74       ,  YES          ,  YES          ,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(302, "PP-Mzda_DanPoj-Dan100",           "302", 40, 0, pomGenItem, exNoMinSalary(75)), //, CZK 75       ,  YES          ,  YES          ,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(303, "PP-Mzda_DanPoj-Dan101",           "303", 40, 0, pomGenItem, exNoMinSalary(100)), //, CZK 100      ,  YES          ,  YES          ,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 

                new TestSpecParams(401, "PP-Mzda_DanPoj-Neodpr064",        "401", 40,  46, pomGenItem, exSalary(20000)), //, CZK 20000    ,  YES          ,  YES          ,  YES          ,  YES          ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(402, "PP-Mzda_DanPoj-Neodpr184",        "402", 40, 184, pomGenItem, exNoMinSalary(20000)), //, CZK 20000    ,  YES          ,  YES          ,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 

                new TestSpecParams(501, "DPC-Mzda_NeUcastZdrav-Prev",      "501", 40, 0, dpcGenItem, exSalaryUcastZdrPrev(-1)), //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(502, "DPC-Mzda_UcastZdrav-Prev",        "502", 40, 0, dpcGenItem, exSalaryUcastZdrPrev(0)),  //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(503, "DPC-Mzda_NeUcastNemoc-Prev",      "503", 40, 0, dpcGenItem, exSalaryUcastNemPrev(-1)), //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(504, "DPC-Mzda_UcastNemoc-Prev",        "504", 40, 0, dpcGenItem, exSalaryUcastNemPrev(0)),  //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(505, "DPP-Mzda_NeUcastZdrav-Prev",      "505", 40, 0, dpcGenItem, exSalaryUcastZdrEmpPrev(-1)), //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(506, "DPP-Mzda_UcastZdrav-Prev",        "506", 40, 0, dpcGenItem, exSalaryUcastZdrEmpPrev(0)),  //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(507, "DPC-Mzda_NeUcastZdrav-Curr",      "507", 40, 0, dpcGenItem, exSalaryUcastZdr(-1)), //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(508, "DPC-Mzda_UcastZdrav-Curr",        "508", 40, 0, dpcGenItem, exSalaryUcastZdr(0)), //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(509, "DPC-Mzda_NeUcastNemoc-Curr",      "509", 40, 0, dpcGenItem, exSalaryUcastNem(-1)), //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(510, "DPC-Mzda_UcastNemoc-Curr",        "510", 40, 0, dpcGenItem, exSalaryUcastNem(0)), //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(511, "DPP-Mzda_NeUcastZdrav-Curr",      "511", 40, 0, dppGenItem, exSalaryUcastZdrEmp(-1)), //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(512, "DPP-Mzda_UcastZdrav-Curr",        "512", 40, 0, dppGenItem, exSalaryUcastZdrEmp(0)), //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 

                new TestSpecParams(601, "DPP-Mzda_NeUcastNemoc-Prev",      "601", 40, 0, dppGenItem, exSalaryUcastNemPrev(-1)), //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(602, "DPP-Mzda_UcastNemoc-Prev",        "602", 40, 0, dppGenItem, exSalaryUcastNemPrev(0)), //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(603, "DPP-Mzda_NeUcastNemoc-Curr",      "603", 40, 0, dppGenItem, exSalaryUcastNem(-1)), //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
                new TestSpecParams(604, "DPP-Mzda_UcastNemoc-Curr",        "604", 40, 0, dppGenItem, exSalaryUcastNem(0)), //,CZK 0,  YES       , NO,  YES          ,  NO           ,  YES          ,  NO            ,  YES               , 0,  NO, NO, NO               ,  NO                   ,  YES             ,  YES             , 
        };

        public static IEnumerable<object[]> TestData => GetTestDecData(_tests);
        public static IEnumerable<object[]> GetTestDecData(TestSpecParams[] tests) => tests.Select((tt) => (new object[] { tt }));
        public ServiceTestExamples2013(ITestOutputHelper output)
        {
            this.output = output;

            this._sut = new ServicePayrolex();
            this._leg = new ServiceLegalios();
        }
        public static IPeriod PrevYear(IPeriod period)
        {
            return new Period(period.Year - 1, period.Month);
        }
        public static IBundleProps CurrYearBundle(IServiceLegalios legSvc, IPeriod period)
        {
            var legResult = legSvc.GetBundle(period);
            return legResult.Value;
        }
        public static IBundleProps PrevYearBundle(IServiceLegalios legSvc, IPeriod period)
        {
            var legResult = legSvc.GetBundle(PrevYear(period));
            return legResult.Value;
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void ServiceExamplesTest(TestSpecParams test)
        {
            var testPeriod = new Period(2013,1);
            testPeriod.Code.Should().Be(201301);

            var prevPeriod = PrevYear(testPeriod);
            prevPeriod.Code.Should().Be(201201);

            var testLegalResult = _leg.GetBundle(testPeriod);
            testLegalResult.IsSuccess.Should().Be(true);

            var testRuleset = testLegalResult.Value;

            var prevLegalResult = _leg.GetBundle(prevPeriod);
            prevLegalResult.IsSuccess.Should().Be(true);

            var prevRuleset = prevLegalResult.Value;

            var example = test.gen.CreateExample(testPeriod, testRuleset, prevRuleset, 
                test.id, test.name, test.number, test.schedWeek, test.nonAtten, test.exp);

            output.WriteLine(example.exampleString());

            //foreach (var impLine in example.importString(testPeriod))
            //{
            //    output.WriteLine(impLine);
            //}

            var targets = example.GetSpecTargets(testPeriod);
            var initService = _sut.InitWithPeriod(testPeriod);
            initService.Should().BeTrue();

            var restService = _sut.GetResults(testPeriod, testRuleset, targets);
            restService.Count().Should().NotBe(0);

            foreach (var (result, index) in restService.Select((item, index) => (item, index)))
            {
                if (result.IsSuccess)
                {
                    var resultValue = result.Value;
                    var articleSymbol = resultValue.ArticleDescr();
                    var conceptSymbol = resultValue.ConceptDescr();
                    output.WriteLine("Index: {0}, ART: {1}, CON: {2}, Result: {3}", index, articleSymbol, conceptSymbol, resultValue.ResultMessage());
                }
                else if (result.IsFailure)
                {
                    var errorValue = result.Error;
                    var articleSymbol = errorValue.ArticleDescr();
                    var conceptSymbol = errorValue.ConceptDescr();
                    output.WriteLine("Index: {0}, ART: {1}, CON: {2}, Error: {3}", index, articleSymbol, conceptSymbol, errorValue.Description());
                }
            }
        }
    }
}
