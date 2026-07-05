namespace SharpClaw.Providers.LocalCommon;

/// <summary>
/// Provider-side path validation for local model file operations.
/// </summary>
public static class PathGuard
{
    public static string EnsureContainedIn(string combined, string parentDir)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(combined);
        ArgumentException.ThrowIfNullOrWhiteSpace(parentDir);

        if (combined.Contains('\0') || parentDir.Contains('\0'))
            throw new InvalidOperationException("Path contains null bytes.");

        var canonical = Path.GetFullPath(combined);
        var canonicalParent = Path.GetFullPath(parentDir)
            .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        var canonicalParentWithSep = canonicalParent + Path.DirectorySeparatorChar;

        if (!canonical.Equals(canonicalParent, StringComparison.OrdinalIgnoreCase)
            && !canonical.StartsWith(canonicalParentWithSep, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                $"Path '{combined}' escapes the allowed directory '{parentDir}'.");
        }

        return canonical;
    }

    public static string EnsureFileName(string name, string paramName = "name")
    {
        ArgumentNullException.ThrowIfNull(name, paramName);

        if (name.Length == 0)
            throw new ArgumentException("File name cannot be empty.", paramName);

        if (name.Contains('\0'))
            throw new ArgumentException("File name contains null bytes.", paramName);

        if (name.Contains("..") || name.Contains('/') || name.Contains('\\'))
            throw new ArgumentException(
                $"File name '{name}' must not contain path separators or traversal sequences.",
                paramName);

        return name;
    }
}
