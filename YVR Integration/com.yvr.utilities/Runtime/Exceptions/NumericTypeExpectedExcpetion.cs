using System;

public class NumericTypeExpectedException : Exception
{
    public NumericTypeExpectedException() { }
    public NumericTypeExpectedException(string message) : base(message) { }
}