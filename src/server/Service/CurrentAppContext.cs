using Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using static Core.Context;

namespace Service;

public class CurrentAppContext: ICurrentAppContext
{
    public IUserIdentity UserIdentity() => Context.OnCurrent<IUserIdentity>.Get();
}
