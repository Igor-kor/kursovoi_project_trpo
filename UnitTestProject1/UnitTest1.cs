using System;
using System.Windows.Forms;
using kursovoi_project_trpo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Login form = new Login();
            Assert.IsTrue(form.Auth("admin","admin")); 
            Assert.IsFalse(form.Auth("admin1 "," admin "));
            Assert.IsFalse(form.Auth("admi"," admin"));
            Assert.IsFalse(form.Auth("true"," true")); 
        }

        [TestMethod]
        public void TestMethod2()
        {
            Clinic clinic = new Clinic();
            clinic.Clinic_Load(new object(), new EventArgs());
            string name = clinic.FindMinDay();
            clinic.button7_Click(new object(),new EventArgs());
            Assert.IsTrue(name != clinic.FindMinDay());
        }
    }
}
