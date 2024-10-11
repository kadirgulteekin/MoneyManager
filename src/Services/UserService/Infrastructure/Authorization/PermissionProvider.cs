﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authorization;

internal sealed class PermissionProvider
{
    public Task<HashSet<string>> GetForUserIdAsync(Guid userId)
    {
        HashSet<string> permissionsSet = [];
        return Task.FromResult(permissionsSet);
    }
}
