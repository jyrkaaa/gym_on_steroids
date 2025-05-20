namespace App.DAL.DataSeeding;

public static class InitialData
{
    public static readonly (string roleName, Guid? id)[]
        Roles =
        [
            ("admin", null),
        ];

    public static readonly (string name, string password, Guid? id, string[] roles)[]
        Users =
        [
            ("admin@taltech.ee", "Foo.Bar.1", null, ["admin"]),
            ("jurgen@gmail.com", "StrongPa1!ssword", null, []),
        ];

    public static readonly (Guid? id, string Name, string createdBy, DateTimeOffset CreatedAt,string? ChangedBy, DateTimeOffset? ChangedAt,string? SysNotes )[]
        Categories =
        [
            (Guid.NewGuid(), "Legs", "system", DateTimeOffset.UtcNow, null, null, null),
            (Guid.NewGuid(), "Push and Chest", "system", DateTimeOffset.UtcNow, null, null, null),
        ];

}