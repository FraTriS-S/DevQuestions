﻿using System.Text.Json;
using Shared;

namespace DevQuestions.Application.Exceptions;

public class NotFoundException : Exception
{
    protected NotFoundException(params Error[] error)
        : base(JsonSerializer.Serialize(error))
    {
    }
}