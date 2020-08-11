using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.D0036;
using R5T.T0010;


namespace R5T.D0035.Default
{
    public class LocalRepositoriesWithRemoteUpdatesProvider : ILocalRepositoriesWithRemoteUpdatesProvider
    {
        private ISourceControlOperator SourceControlOperator { get; }


        public LocalRepositoriesWithRemoteUpdatesProvider(
            ISourceControlOperator sourceControlOperator)
        {
            this.SourceControlOperator = sourceControlOperator;
        }

        public async Task<LocalRepositoriesWithRemoteUpdatesList> GetLocalRepositoriesWithRemoteUpdatesList(IEnumerable<LocalRepositoryDirectoryPath> localRepositoryDirectoryPaths)
        {
            var output = new LocalRepositoriesWithRemoteUpdatesList();

            foreach (var localRepositoryDirectoryPath in localRepositoryDirectoryPaths)
            {
                var hasRemoteChangesNotInLocal = await this.SourceControlOperator.HasRemoteChangesNotInLocal(localRepositoryDirectoryPath); // Sequentially asynchronous.
                if(hasRemoteChangesNotInLocal)
                {
                    output.LocalRepositoryDirectoryPaths.Add(localRepositoryDirectoryPath);
                }
            }

            return output;
        }
    }
}
