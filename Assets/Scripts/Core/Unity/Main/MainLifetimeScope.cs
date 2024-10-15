﻿using Core.Network.Domain;
using Core.Network.Infrastructure;
using MessagePipe;
using SoapTools.SceneController.Application.Repository;
using VContainer;
using VContainer.Unity;

namespace Core.Unity.Main
{
	public class MainLifetimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			RegisterMessagePipe(builder);
			RegisterNetwork(builder);
			RegisterScene(builder);
		}

		private void RegisterNetwork(IContainerBuilder builder)
		{
			builder.Register<RoomPlayer>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
		}

		private void RegisterScene(IContainerBuilder builder)
		{
			builder.Register<SceneRepository>(Lifetime.Singleton);
		}

		private void RegisterMessagePipe(IContainerBuilder builder)
		{
			builder.RegisterMessagePipe();

			builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
		}
	}
}