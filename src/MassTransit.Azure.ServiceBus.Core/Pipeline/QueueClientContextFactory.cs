// Copyright 2007-2018 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.Azure.ServiceBus.Core.Pipeline
{
    using System;
    using Contexts;
    using GreenPipes;
    using Transport;


    public class QueueClientContextFactory :
        ClientContextFactory
    {
        readonly ReceiveSettings _settings;

        public QueueClientContextFactory(IMessagingFactoryContextSupervisor messagingFactoryContextSupervisor, INamespaceContextSupervisor namespaceContextSupervisor, IPipe<MessagingFactoryContext> messagingFactoryPipe,
            IPipe<NamespaceContext> namespacePipe, ReceiveSettings settings)
            : base(messagingFactoryContextSupervisor, namespaceContextSupervisor, messagingFactoryPipe, namespacePipe, settings)
        {
            _settings = settings;
        }

        protected override ClientContext CreateClientContext(MessagingFactoryContext connectionContext, Uri inputAddress)
        {
            var messageReceiver = connectionContext.MessagingFactory.CreateMessageReceiver(_settings.Path);
            messageReceiver.PrefetchCount = _settings.PrefetchCount;

            return new QueueClientContext(messageReceiver, inputAddress, _settings);
        }
    }
}