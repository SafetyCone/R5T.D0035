﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.T0010;


namespace R5T.D0035
{
    /// <summary>
    /// Provides the artifact containing the list of local repositories that have remote updates in a strongly-typed, parameterless way.
    /// For Git, this would be local repositories whose local master branch is behind the origin/master branch.
    /// </summary>
    public interface ILocalRepositoriesWithRemoteUpdatesProvider
    {
        Task<LocalRepositoriesWithRemoteUpdatesList> GetLocalRepositoriesWithRemoteUpdatesList(IEnumerable<LocalRepositoryDirectoryPath> localRepositoryDirectoryPaths);
    }
}
