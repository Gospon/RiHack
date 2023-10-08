﻿

namespace Module.Application.Types;

public class Response<T>
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Data { get; set; }
}
