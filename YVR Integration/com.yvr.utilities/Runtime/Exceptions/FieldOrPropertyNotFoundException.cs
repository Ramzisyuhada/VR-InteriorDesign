using System;

public class FieldNotFoundException : Exception
{
    public FieldNotFoundException() { }

    public FieldNotFoundException(string message) : base(message) { }
}

public class PropertyNotFoundException : Exception
{
    public PropertyNotFoundException() { }

    public PropertyNotFoundException(string message) : base(message) { }
}

public class FieldNorPropertyNotFoundException : Exception
{
    public FieldNorPropertyNotFoundException() { }

    public FieldNorPropertyNotFoundException(string message) : base(message) { }
}
