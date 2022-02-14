using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.D0010;
using R5T.D0036;
using R5T.T0001;
using R5T.T0010;


namespace R5T.D0035.Default
{
    public class LocalRepositoriesWithRemoteUpdatesProvider : ILocalRepositoriesWithRemoteUpdatesProvider
    {
        private IMessageSink MessageSink { get; }
        private ISourceControlOperator SourceControlOperator { get; }


        public LocalRepositoriesWithRemoteUpdatesProvider(
            IMessageSink messageSink,
            ISourceControlOperator sourceControlOperator)
        {
            this.MessageSink = messageSink;
            this.SourceControlOperator = sourceControlOperator;
        }

        public async Task<LocalRepositoriesWithRemoteUpdatesList> GetLocalRepositoriesWithRemoteUpdatesList(IEnumerable<LocalRepositoryDirectoryPath> localRepositoryDirectoryPaths)
        {
            var output = new LocalRepositoriesWithRemoteUpdatesList();

            foreach (var localRepositoryDirectoryPath in localRepositoryDirectoryPaths)
            {
                var messaging = this.MessageSink.AddAsync(Message.NewOutput(DateTime.UtcNow, $"{localRepositoryDirectoryPath}"));

                try
                {
                    var hasRemoteChangesNotInLocal = await this.SourceControlOperator.HasRemoteChangesNotInLocal(localRepositoryDirectoryPath); // Sequentially asynchronous.
                    if (hasRemoteChangesNotInLocal)
                    {
                        output.LocalRepositoryDirectoryPaths.Add(localRepositoryDirectoryPath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                await messaging;
            }

            return output;
        }
    }
}
