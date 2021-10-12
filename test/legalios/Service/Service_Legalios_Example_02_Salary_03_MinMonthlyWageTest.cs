﻿using System;
using AutoFixture;
using FluentAssertions;
using HraveMzdy.Legalios.Service;
using HraveMzdy.Legalios.Interfaces;
using HraveMzdy.Legalios.Service.Types;
using NSubstitute;
using System.Collections.Generic;
using Xunit;
using LegaliosTests;

namespace LegaliosTest.Service
{
    [Collection("TestEngine")]
    public class Service_Legalios_Example_02_Salary_03_MinMonthlyWageTest : Service_Legalios_Example_SalaryTest
    {

        private readonly IServiceLegalios _sut;

        private static readonly TestIntScenario[] _tests = new TestIntScenario[]
        {
            new TestIntScenario("2011", new TestIntParams[] {
                new TestIntParams( "2011-1", 2011, 1, 2011, 1, 8000 ),
                new TestIntParams( "2011-2", 2011, 2, 2011, 2, 8000 ),
                new TestIntParams( "2011-3", 2011, 3, 2011, 3, 8000 ),
                new TestIntParams( "2011-4", 2011, 4, 2011, 4, 8000 ),
                new TestIntParams( "2011-5", 2011, 5, 2011, 5, 8000 ),
                new TestIntParams( "2011-6", 2011, 6, 2011, 6, 8000 ),
                new TestIntParams( "2011-7", 2011, 7, 2011, 7, 8000 ),
                new TestIntParams( "2011-8", 2011, 8, 2011, 8, 8000 ),
                new TestIntParams( "2011-9", 2011, 9, 2011, 9, 8000 ),
                new TestIntParams( "2011-10", 2011, 10, 2011, 10, 8000 ),
                new TestIntParams( "2011-11", 2011, 11, 2011, 11, 8000 ),
                new TestIntParams( "2011-12", 2011, 12, 2011, 12, 8000 ),
            }),
            new TestIntScenario("2012", new TestIntParams[] {
                new TestIntParams( "2012-1", 2012, 1, 2012, 1, 8000 ),
                new TestIntParams( "2012-2", 2012, 2, 2012, 2, 8000 ),
                new TestIntParams( "2012-3", 2012, 3, 2012, 3, 8000 ),
                new TestIntParams( "2012-4", 2012, 4, 2012, 4, 8000 ),
                new TestIntParams( "2012-5", 2012, 5, 2012, 5, 8000 ),
                new TestIntParams( "2012-6", 2012, 6, 2012, 6, 8000 ),
                new TestIntParams( "2012-7", 2012, 7, 2012, 7, 8000 ),
                new TestIntParams( "2012-8", 2012, 8, 2012, 8, 8000 ),
                new TestIntParams( "2012-9", 2012, 9, 2012, 9, 8000 ),
                new TestIntParams( "2012-10", 2012, 10, 2012, 10, 8000 ),
                new TestIntParams( "2012-11", 2012, 11, 2012, 11, 8000 ),
                new TestIntParams( "2012-12", 2012, 12, 2012, 12, 8000 ),
            }),
            new TestIntScenario("2013", new TestIntParams[] {
                new TestIntParams( "2013-1", 2013, 1, 2013, 1, 8000 ),
                new TestIntParams( "2013-2", 2013, 2, 2013, 2, 8000 ),
                new TestIntParams( "2013-3", 2013, 3, 2013, 3, 8000 ),
                new TestIntParams( "2013-4", 2013, 4, 2013, 4, 8000 ),
                new TestIntParams( "2013-5", 2013, 5, 2013, 5, 8000 ),
                new TestIntParams( "2013-6", 2013, 6, 2013, 6, 8000 ),
                new TestIntParams( "2013-7", 2013, 7, 2013, 7, 8000 ),
                new TestIntParams( "2013-8", 2013, 8, 2013, 8, 8500 ),
                new TestIntParams( "2013-9", 2013, 9, 2013, 9, 8500 ),
                new TestIntParams( "2013-10", 2013, 10, 2013, 10, 8500 ),
                new TestIntParams( "2013-11", 2013, 11, 2013, 11, 8500 ),
                new TestIntParams( "2013-12", 2013, 12, 2013, 12, 8500 ),
            }),
            new TestIntScenario("2014", new TestIntParams[] {
                new TestIntParams( "2014-1", 2014, 1, 2014, 1, 8500 ),
                new TestIntParams( "2014-2", 2014, 2, 2014, 2, 8500 ),
                new TestIntParams( "2014-3", 2014, 3, 2014, 3, 8500 ),
                new TestIntParams( "2014-4", 2014, 4, 2014, 4, 8500 ),
                new TestIntParams( "2014-5", 2014, 5, 2014, 5, 8500 ),
                new TestIntParams( "2014-6", 2014, 6, 2014, 6, 8500 ),
                new TestIntParams( "2014-7", 2014, 7, 2014, 7, 8500 ),
                new TestIntParams( "2014-8", 2014, 8, 2014, 8, 8500 ),
                new TestIntParams( "2014-9", 2014, 9, 2014, 9, 8500 ),
                new TestIntParams( "2014-10", 2014, 10, 2014, 10, 8500 ),
                new TestIntParams( "2014-11", 2014, 11, 2014, 11, 8500 ),
                new TestIntParams( "2014-12", 2014, 12, 2014, 12, 8500 ),
            }),
            new TestIntScenario("2015", new TestIntParams[] {
                new TestIntParams( "2015-1", 2015, 1, 2015, 1, 9200 ),
                new TestIntParams( "2015-2", 2015, 2, 2015, 2, 9200 ),
                new TestIntParams( "2015-3", 2015, 3, 2015, 3, 9200 ),
                new TestIntParams( "2015-4", 2015, 4, 2015, 4, 9200 ),
                new TestIntParams( "2015-5", 2015, 5, 2015, 5, 9200 ),
                new TestIntParams( "2015-6", 2015, 6, 2015, 6, 9200 ),
                new TestIntParams( "2015-7", 2015, 7, 2015, 7, 9200 ),
                new TestIntParams( "2015-8", 2015, 8, 2015, 8, 9200 ),
                new TestIntParams( "2015-9", 2015, 9, 2015, 9, 9200 ),
                new TestIntParams( "2015-10", 2015, 10, 2015, 10, 9200 ),
                new TestIntParams( "2015-11", 2015, 11, 2015, 11, 9200 ),
                new TestIntParams( "2015-12", 2015, 12, 2015, 12, 9200 ),
            }),
            new TestIntScenario("2016", new TestIntParams[] {
                new TestIntParams( "2016-1", 2016, 1, 2016, 1, 9900 ),
                new TestIntParams( "2016-2", 2016, 2, 2016, 2, 9900 ),
                new TestIntParams( "2016-3", 2016, 3, 2016, 3, 9900 ),
                new TestIntParams( "2016-4", 2016, 4, 2016, 4, 9900 ),
                new TestIntParams( "2016-5", 2016, 5, 2016, 5, 9900 ),
                new TestIntParams( "2016-6", 2016, 6, 2016, 6, 9900 ),
                new TestIntParams( "2016-7", 2016, 7, 2016, 7, 9900 ),
                new TestIntParams( "2016-8", 2016, 8, 2016, 8, 9900 ),
                new TestIntParams( "2016-9", 2016, 9, 2016, 9, 9900 ),
                new TestIntParams( "2016-10", 2016, 10, 2016, 10, 9900 ),
                new TestIntParams( "2016-11", 2016, 11, 2016, 11, 9900 ),
                new TestIntParams( "2016-12", 2016, 12, 2016, 12, 9900 ),
            }),
            new TestIntScenario("2017", new TestIntParams[] {
                new TestIntParams( "2017-1", 2017, 1, 2017, 1, 11000 ),
                new TestIntParams( "2017-2", 2017, 2, 2017, 2, 11000 ),
                new TestIntParams( "2017-3", 2017, 3, 2017, 3, 11000 ),
                new TestIntParams( "2017-4", 2017, 4, 2017, 4, 11000 ),
                new TestIntParams( "2017-5", 2017, 5, 2017, 5, 11000 ),
                new TestIntParams( "2017-6", 2017, 6, 2017, 6, 11000 ),
                new TestIntParams( "2017-7", 2017, 7, 2017, 7, 11000 ),
                new TestIntParams( "2017-8", 2017, 8, 2017, 8, 11000 ),
                new TestIntParams( "2017-9", 2017, 9, 2017, 9, 11000 ),
                new TestIntParams( "2017-10", 2017, 10, 2017, 10, 11000 ),
                new TestIntParams( "2017-11", 2017, 11, 2017, 11, 11000 ),
                new TestIntParams( "2017-12", 2017, 12, 2017, 12, 11000 ),
            }),
            new TestIntScenario("2018", new TestIntParams[] {
                new TestIntParams( "2018-1", 2018, 1, 2018, 1, 12200 ),
                new TestIntParams( "2018-2", 2018, 2, 2018, 2, 12200 ),
                new TestIntParams( "2018-3", 2018, 3, 2018, 3, 12200 ),
                new TestIntParams( "2018-4", 2018, 4, 2018, 4, 12200 ),
                new TestIntParams( "2018-5", 2018, 5, 2018, 5, 12200 ),
                new TestIntParams( "2018-6", 2018, 6, 2018, 6, 12200 ),
                new TestIntParams( "2018-7", 2018, 7, 2018, 7, 12200 ),
                new TestIntParams( "2018-8", 2018, 8, 2018, 8, 12200 ),
                new TestIntParams( "2018-9", 2018, 9, 2018, 9, 12200 ),
                new TestIntParams( "2018-10", 2018, 10, 2018, 10, 12200 ),
                new TestIntParams( "2018-11", 2018, 11, 2018, 11, 12200 ),
                new TestIntParams( "2018-12", 2018, 12, 2018, 12, 12200 ),
            }),
            new TestIntScenario("2019", new TestIntParams[] {
                new TestIntParams( "2019-1", 2019, 1, 2019, 1, 13350 ),
                new TestIntParams( "2019-2", 2019, 2, 2019, 2, 13350 ),
                new TestIntParams( "2019-3", 2019, 3, 2019, 3, 13350 ),
                new TestIntParams( "2019-4", 2019, 4, 2019, 4, 13350 ),
                new TestIntParams( "2019-5", 2019, 5, 2019, 5, 13350 ),
                new TestIntParams( "2019-6", 2019, 6, 2019, 6, 13350 ),
                new TestIntParams( "2019-7", 2019, 7, 2019, 7, 13350 ),
                new TestIntParams( "2019-8", 2019, 8, 2019, 8, 13350 ),
                new TestIntParams( "2019-9", 2019, 9, 2019, 9, 13350 ),
                new TestIntParams( "2019-10", 2019, 10, 2019, 10, 13350 ),
                new TestIntParams( "2019-11", 2019, 11, 2019, 11, 13350 ),
                new TestIntParams( "2019-12", 2019, 12, 2019, 12, 13350 ),
            }),
            new TestIntScenario("2020", new TestIntParams[] {
                new TestIntParams( "2020-1", 2020, 1, 2020, 1, 14600 ),
                new TestIntParams( "2020-2", 2020, 2, 2020, 2, 14600 ),
                new TestIntParams( "2020-3", 2020, 3, 2020, 3, 14600 ),
                new TestIntParams( "2020-4", 2020, 4, 2020, 4, 14600 ),
                new TestIntParams( "2020-5", 2020, 5, 2020, 5, 14600 ),
                new TestIntParams( "2020-6", 2020, 6, 2020, 6, 14600 ),
                new TestIntParams( "2020-7", 2020, 7, 2020, 7, 14600 ),
                new TestIntParams( "2020-8", 2020, 8, 2020, 8, 14600 ),
                new TestIntParams( "2020-9", 2020, 9, 2020, 9, 14600 ),
                new TestIntParams( "2020-10", 2020, 10, 2020, 10, 14600 ),
                new TestIntParams( "2020-11", 2020, 11, 2020, 11, 14600 ),
                new TestIntParams( "2020-12", 2020, 12, 2020, 12, 14600 ),
            }),
            new TestIntScenario("2021", new TestIntParams[] {
                new TestIntParams( "2021-1", 2021, 1, 2021, 1, 15200 ),
                new TestIntParams( "2021-2", 2021, 2, 2021, 2, 15200 ),
                new TestIntParams( "2021-3", 2021, 3, 2021, 3, 15200 ),
                new TestIntParams( "2021-4", 2021, 4, 2021, 4, 15200 ),
                new TestIntParams( "2021-5", 2021, 5, 2021, 5, 15200 ),
                new TestIntParams( "2021-6", 2021, 6, 2021, 6, 15200 ),
                new TestIntParams( "2021-7", 2021, 7, 2021, 7, 15200 ),
                new TestIntParams( "2021-8", 2021, 8, 2021, 8, 15200 ),
                new TestIntParams( "2021-9", 2021, 9, 2021, 9, 15200 ),
                new TestIntParams( "2021-10", 2021, 10, 2021, 10, 15200 ),
                new TestIntParams( "2021-11", 2021, 11, 2021, 11, 15200 ),
                new TestIntParams( "2021-12", 2021, 12, 2021, 12, 15200 ),
            }),
            new TestIntScenario("2022", new TestIntParams[] {
                new TestIntParams( "2022-1", 2022, 1, 2022, 1, 15200 ),
                new TestIntParams( "2022-2", 2022, 2, 2022, 2, 15200 ),
                new TestIntParams( "2022-3", 2022, 3, 2022, 3, 15200 ),
                new TestIntParams( "2022-4", 2022, 4, 2022, 4, 15200 ),
                new TestIntParams( "2022-5", 2022, 5, 2022, 5, 15200 ),
                new TestIntParams( "2022-6", 2022, 6, 2022, 6, 15200 ),
                new TestIntParams( "2022-7", 2022, 7, 2022, 7, 15200 ),
                new TestIntParams( "2022-8", 2022, 8, 2022, 8, 15200 ),
                new TestIntParams( "2022-9", 2022, 9, 2022, 9, 15200 ),
                new TestIntParams( "2022-10", 2022, 10, 2022, 10, 15200 ),
                new TestIntParams( "2022-11", 2022, 11, 2022, 11, 15200 ),
                new TestIntParams( "2022-12", 2022, 12, 2022, 12, 15200 ),
            }),
        };
        public static IEnumerable<object[]> TestData => GetTestIntData(_tests);
        public Service_Legalios_Example_02_Salary_03_MinMonthlyWageTest()
        {
            _sut = new ServiceLegalios();
#if __PROTOKOL_TEST_FILE__
            //02_Salary_03_MinMonthlyWage
            LogTestExamples("02_Salary_03_MinMonthlyWage.txt", _tests);
#endif
        }
        [Theory]
        [MemberData(nameof(TestData))]
        public void GetBundle_ShouldReturnValid_MinMonthlyWage(string testTitle, string testName, Int16 testYear, Int16 testMonth, Int16 resultYear, Int16 resultMonth, Int32 resultValue)
        {
            var testPeriod = new Period(testYear, testMonth);

            var testResult = _sut.GetBundle(testPeriod);

            ShoulBeValidBundle(testResult, resultYear, resultMonth);

            testResult.Value.SalaryProps.MinMonthlyWage.Should().Be(resultValue, "Because Period: {0} - {1}", testTitle, testName);
        }
    }
}
