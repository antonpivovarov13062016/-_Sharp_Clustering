using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryClustering;
//using WFASeasonalCoefficients;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTestClustering
    {
        [TestMethod]
        public void TestMethodNormalizedData()
        {
           Double normEl = ClassMath.NormalizedData(50);
           Double normElRound = Math.Round(normEl, 9);
           Assert.IsTrue( normElRound == 0.510204082);
        }

        [TestMethod]
        public void TestMethodNormalizedDataList()
        {
            Double min;
            Double max;

            bool res = ClassMath.MinMax(ClassForTest.DataTest(), out min, out max);
            List<Double> listOut = ClassMath.NormalizedData(ClassForTest.DataTest());

            Assert.IsTrue(res);
            for (int i = 0; i < listOut.Count; i++)
            {
                Assert.IsTrue(Math.Round(listOut[i], 9) == ClassForTest.DataTestResNormalized()[i]);
            }
        }

        [TestMethod]
        public void TestMethodMinMax()
        {
            Double min;
            Double max;

            bool res = ClassMath.MinMax(ClassForTest.DataTest(),out min,out max);

            Assert.IsTrue(res);
            Assert.IsTrue(min == 2);
            Assert.IsTrue(max == 100);
        }

        [TestMethod]
        public void TestMethodMinMaxList()
        {
            Double min;
            Double max;

            List<List<Double>> res = ClassMath.MinMax(ClassForTest.DataTestClusteringInput());

            Assert.IsTrue(res[0][0] == 188);
            Assert.IsTrue(res[0][1] == 777);

            Assert.IsTrue(res[1][0] == 113);
            Assert.IsTrue(res[1][1] == 596);
        }

        [TestMethod]
        public void TestMethodZscaling()
        {

            List<Double> listOut = ClassMath.Zscaling(ClassForTest.DataTest());
            for (int i = 0; i < listOut.Count; i++)
            {
                Assert.IsTrue(Math.Round(listOut[i], 9) == ClassForTest.DataTestResZscaling()[i]);
            }
        }

        [TestMethod]
        public void TestMethodTballs()
        {

            List<Double> listOut = ClassMath.Zscaling(ClassForTest.DataTest());
            List<Double> listOutTballs = ClassMath.Tballs(listOut);

            for (int i = 0; i < listOutTballs.Count; i++)
            {
                Assert.IsTrue(Math.Round(listOutTballs[i], 8) == ClassForTest.DataTestResTballs()[i]);
            }
        }

        [TestMethod]
        public void TestMethodEuclideanDistance()
        {
            List<Double> listXY = new List<double>() { 777, 113};
            List<Double> listCentrXY = new List<double>() { 120, 456};
       
            Assert.IsTrue(Math.Round(ClassMath.EuclideanDistance(listXY, listCentrXY), 7) == 741.1464093);
        }

        [TestMethod]
        public void TestMethodEuclideanDistance2()
        {
            List<Double> listXY = new List<double>() { 32, 43 };
            List<Double> listCentrXY = new List<double>() { 23, 11 };

            Assert.IsTrue(Math.Round(ClassMath.EuclideanDistance(listXY, listCentrXY), 8) == 33.24154028);
        }


        //[TestMethod]
        //public void TestMethodEuclideanDistance3()
        //{

        //    Assert.IsTrue(Math.Round(ClassMath.EuclideanDistance(206.753207687061, 157.1156772), 6) == 2463.884436);
        //}

        [TestMethod]
        public void TestMethodChebyshevDistance()
        {
            List<Double> listXY = new List<double>() { 32, 43 };
            List<Double> listCentrXY = new List<double>() { 23, 11 };

            Assert.IsTrue(Math.Round(ClassMath.ChebyshevDistance(listXY, listCentrXY)) == 32);
        }

        [TestMethod]
        public void TestMethodManhattanDistance()
        {
            List<Double> listXY = new List<double>() { 32, 43 };
            List<Double> listCentrXY = new List<double>() { 23, 11 };

            Assert.IsTrue(Math.Round(ClassMath.ManhattanDistance(listXY, listCentrXY)) == 41);
        }

        [TestMethod]
        public void TestMethodClusteringDistance()
        {
            ClassClustering cC = new ClassClustering(ClassForTest.DataTestClusteringInput(), ClassForTest.DataTestClusteringCenter());
            List<List<Double>> lDistance = cC.Distance;

            for (int i = 0; i < lDistance.Count; i++)
            {
                for (int j = 0; j < lDistance[i].Count; j++)
                {
                    Assert.IsTrue(Math.Round(ClassForTest.DataTestEuclidDistanceResult()[i][j],7) == Math.Round( lDistance[i][j],7));
                }
            }
        }

        [TestMethod]
        public void TestMethodClusteringIndex()
        {
            ClassClustering cC = new ClassClustering(ClassForTest.DataTestClusteringInput(), ClassForTest.DataTestClusteringCenter());
            List<long> indexCluster = cC.IndexCluster;
            
            for (int i = 0; i < indexCluster.Count; i++)
            {
                    Assert.IsTrue(ClassForTest.DataTestIndexClusterResult()[i] == indexCluster[i]+1 );
            }
        }

        [TestMethod]
        public void TestMethodClusteringCenter()
        {
            ClassClustering cC = new ClassClustering(ClassForTest.DataTestClusteringInput(), ClassForTest.DataTestClusteringCenter());
            List<List<Double>> lNewCenter = cC.NewCenterCluster;

            for (int i = 0; i < lNewCenter.Count; i++)
            {
                for (int j = 0; j < lNewCenter[i].Count; j++)
                {
                    Assert.IsTrue(ClassForTest.DataTestCenterClusterResult()[i][j] == Math.Round(lNewCenter[i][j],7));
                }
            }
        }

        [TestMethod]
        public void TestMethodClusteringCenterAndNewDistance()
        {
            List<Double> listXY = new List<double>() { 326.3333333,  472.3333333 };
            List<Double> listCentrXY = new List<double>() { 120, 456 };

            Assert.IsTrue(Math.Round(ClassMath.EuclideanDistance(listXY, listCentrXY), 7) == 206.9787965);
        }

        [TestMethod]
        public void TestMethodFindCenterCluster()
        {
            ClassClustering cC = new ClassClustering(ClassForTest.DataTestClusteringInput(), ClassForTest.DataTestClusteringCenter());
            List<List<Double>> lCenter = cC.FindCenterCluster();
            List<List<Double>> lDistance = cC.Distance;


            for (int i = 0; i < lCenter.Count; i++)
            {
                for (int j = 0; j < lCenter[i].Count; j++)
                {
                    Assert.IsTrue(Math.Round(ClassForTest.DataTestFindCenterCluster()[i][j], 7) == Math.Round(lCenter[i][j], 7));
                }
            }

            for (int i = 0; i < lDistance.Count; i++)
            {
                for (int j = 0; j < lDistance[i].Count; j++)
                {
                    Assert.IsTrue(Math.Round(ClassForTest.DataTestFindCenterClusterDistance()[i][j], 7) == Math.Round(lDistance[i][j], 7));
                }
            }
        }
        
        [TestMethod]
        public void TestMethodSko()
        {
            ClassClustering cC = new ClassClustering(ClassForTest.DataTestClusteringInput(), ClassForTest.DataTestClusteringCenter());
            List<List<Double>> lCenter = cC.FindCenterCluster();
            Double sko = cC.CheckSKO();
            Assert.IsTrue(Math.Round(sko, 6) == 3769.325296);
        }

        [TestMethod]
        public void TestMethodCloneList()
        {
            List<int> l1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            List<int> l2 = new List<int>(l1);
            l2[4] = 13;
            Assert.AreNotEqual(l1, l2);
        }

        [TestMethod]
        public void TestMethodRand()
        {
            List<int> l1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            List<int> l2 = new List<int>(l1);
            l2[4] = 13;
            Assert.AreNotEqual(l1, l2);
        }

        //Double r = GetRandomIntBetweenDouble(0, 1000);
        //Double r2 = GetRandomIntBetweenDouble(500, 1000);
        //Double r3 = GetRandomIntBetweenDouble(0, 1000);
        //Double r4 = GetRandomIntBetweenDouble(-10000, 10);

    }
}
