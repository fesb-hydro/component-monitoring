﻿namespace Fesb.Hydrocontest.Infrastructure.Extensions
{
    public static class ObjectExtension
    {
        public static bool IsNull(this object value) => value == null;
        public static bool IsNotNull(this object value) => value != null;
    }
}
