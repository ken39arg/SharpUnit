/**
 * @file TestException.cs
 * 
 * Unit testing exception.
 * Allows us to set a description about the failed test and 
 * distinguish from the regular System.Exceptions.
 */

using System;
using System.Diagnostics;

namespace SharpUnit
{
    public class TestException : Exception
    {
        // Member values
        private string m_desc = null;       // Description of the failed test (i.e. the class and method name)
        private StackFrame m_frame = null;  // Stack frame where error occurred.    

        /**
         * Get / set the Description property.
         */
        public string Description
        {
            get { return m_desc; }
            set { m_desc = value; }
        }

        /**
         * Get the stack frame property
         */
        public StackFrame StackFrame
        {
            get { return m_frame; }
        }

        /**
         * Constructor
         * 
         * @param msg, error message to display.
         */
        public TestException(string msg)
            : base(msg)
        {
            // Set ignoreList to capture the correct level at which the exception was thrown
            //      - TestException constructor
            //      - Assert class method
            //      - [UnityTestCase method]
            //      - TestCase method               <-- the level we want
            string[] ignoreList = {"/TestException.cs", "/Assert.cs", "/UnityTestCase.cs"};

            // If the stack trace to this point is valid
            StackTrace trace = new StackTrace(true);
            if (null != trace)
            {
                // Iterate through stack frames
                StackFrame frame = null;
                for (int index = 0; index < trace.FrameCount; ++index)
                {
                    frame = trace.GetFrame(index);
                    if (frame == null) 
                    {
                        goto next;
                    }
                    foreach (string ignorefile in ignoreList)
                    {
                        if (0 < frame.GetFileName().IndexOf(ignorefile)) 
                        {
                            goto next;
                        }
                    }
                    m_frame = frame;
                    break;

                    next:
                        continue;
                }
            }
        }
    }
}
