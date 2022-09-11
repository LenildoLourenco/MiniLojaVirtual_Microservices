﻿namespace MiniLojaVirtual.IdentityServer.SeedDatabase;

public interface IDatabaseSeedInitializer
{
    void InitializeSeedRoles();
    void InitializeSeedUsers();
}
