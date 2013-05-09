/**
 * @file TestCase.cs
 *
 * This class defines a "test case."
 * A test case is a class that contains several methods that test and
 * verify the expected functionality of another class using the Assert methods.
 */

using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Object = System.Object;

namespace SharpUnit
{
    public class UnityTestCase : MonoBehaviour, ITestCase
    {
        // Members
        private string m_testMethod = null;     // Name of the test method to run.
        private Exception m_caughtEx = null;    // Exception thrown by unit test method.
        private TestResult _TestResult = null;
        public TestResult GetTestResult() {
            return _TestResult;
        }

        protected List<TestException> m_ExList = new List<TestException>();
        protected void MarkAsFailure(TestException e) 
        {
            e.Description  = "Failed: " + GetType() + "." + m_testMethod + "()";
            if (null != e.StackFrame)
            {
                // Add stack frame info
                e.Description += " in File: " + System.IO.Path.GetFileName( e.StackFrame.GetFileName() );
                e.Description += " on Line: " + e.StackFrame.GetFileLineNumber();
            }
            m_ExList.Add(e);
        }

        protected void DoneTesting() {
        }

        /**
         * Perform any setup before the test is run.
         */
        public virtual void SetUp()
        {
            // Base class has nothing to setup.
        }

        /**
         * Perform any clean up after the test has run.
         */
        public virtual void TearDown()
        {
            // Base class has nothing to tear down.
        }

        /**
         * Set the name of the test method to run.
         *
         * @param method, the test method to run.
         */
        public void SetTestMethod(string method)
        {
            m_testMethod = method;
        }

        /**
         * Run the test, catching all exceptions.
         * 
         * @param result, the result of the test.
         * 
         * @return TestResult, the result of the test.
         */
        public IEnumerator Run(TestResult result)
        {
            // If test method invalid
            if (null == m_testMethod)
            {
                // Error
                throw new Exception("Invalid test method encountered, be sure to call " +
                                    "TestCase::SetTestMethod()");
            }

            // If the test method does not exist
            Type type = GetType();
            MethodInfo method = type.GetMethod(m_testMethod);
            if (null == method)
            {
                // Error
                throw new Exception("Test method: " + m_testMethod + " does not exist in class: " +
                                    type.ToString());
            }

            // If result invalid
            if (null == result)
            {
                // Create result
                result = new TestResult();
            }

            // Pre-test setup
            SetUp();
            result.TestStarted();

            yield return StartCoroutine((IEnumerator)method.Invoke(this, null));

            if (0 < m_ExList.Count)
            {
                foreach (TestException te in m_ExList)
                {
                    result.TestFailed(te);
                }
                m_ExList.Clear();
                UnityEngine.Debug.LogWarning("[SharpUnit] " + type.Name + "." + method.Name + " failed");

            } else {
                UnityEngine.Debug.Log("[SharpUnit] " + type.Name + "." + method.Name + " runs ok");
            }

            // Clear expected exception
            Assert.Exception = null;

            // Post-test cleanup
            TearDown();

            _TestResult = result;
        }

        #region Assert alias
        public void True(bool boolean)
        {
            try {
                Assert.True(boolean);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void True(bool boolean, string msg)
        {
            try {
                Assert.True(boolean, msg);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void False(bool boolean)
        {
            try {
                Assert.False(boolean);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void False(bool boolean, string msg)
        {
            try {
                Assert.False(boolean, msg);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Null(Object obj)
        {
            try {
                Assert.Null(obj);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Null(Object obj, string msg)
        {
            try {
                Assert.Null(obj, msg);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void NotNull(Object obj)
        {
            try {
                Assert.NotNull(obj);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void NotNull(Object obj, string msg)
        {
            try {
                Assert.NotNull(obj, msg);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Equal(int wanted, int got)
        {
            try {
                Assert.Equal(wanted, got);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Equal(int wanted, int got, string msg)
        {
            try {
                Assert.Equal(wanted, got, msg);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Equal(float wanted, float got)
        {
            try {
                Assert.Equal(wanted, got);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Equal(float wanted, float got, string msg)
        {
            try {
                Assert.Equal(wanted, got, msg);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Equal(string wanted, string got)
        {
            try {
                Assert.Equal(wanted, got);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Equal(string wanted, string got, string msg)
        {
            try {
                Assert.Equal(wanted, got, msg);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Equal(bool wanted, bool got)
        {
            try {
                Assert.Equal(wanted, got);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Equal(bool wanted, bool got, string msg)
        {
            try {
                Assert.Equal(wanted, got, msg);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Equal(Exception wanted, Exception got)
        {
            try {
                Assert.Equal(wanted, got);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Equal(Exception wanted, Exception got, string msg)
        {
            try {
                Assert.Equal(wanted, got, msg);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Equal(Object wanted, Object got)
        {
            try {
                Assert.Equal(wanted, got);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        public void Equal(Object wanted, Object got, string msg)
        {
            try {
                Assert.Equal(wanted, got, msg);
            }
            catch (TestException e)
            {
                MarkAsFailure(e);
            }
        }
        #endregion
    } 
}
