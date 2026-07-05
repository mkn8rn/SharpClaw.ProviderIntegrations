using NUnit.Framework;
using SharpClaw.Providers.LocalCommon;

namespace SharpClaw.ProviderIntegrations.Tests;

[TestFixture]
public sealed class LocalProviderPathGuardTests
{
    [Test]
    public void EnsureContainedInAcceptsPathInsideParentDirectory()
    {
        using var temp = new TemporaryDirectory();
        var child = Path.Combine(temp.Path, "models", "model.gguf");

        var canonical = PathGuard.EnsureContainedIn(child, temp.Path);

        Assert.That(canonical, Is.EqualTo(Path.GetFullPath(child)));
    }

    [Test]
    public void EnsureContainedInRejectsTraversalEscape()
    {
        using var temp = new TemporaryDirectory();
        var escaped = Path.Combine(temp.Path, "..", "model.gguf");

        Assert.Throws<InvalidOperationException>(
            () => PathGuard.EnsureContainedIn(escaped, temp.Path));
    }

    [Test]
    public void EnsureContainedInRejectsSiblingPrefixEscape()
    {
        using var temp = new TemporaryDirectory();
        var sibling = temp.Path + "-sibling";
        var escaped = Path.Combine(sibling, "model.gguf");

        Assert.Throws<InvalidOperationException>(
            () => PathGuard.EnsureContainedIn(escaped, temp.Path));
    }

    [Test]
    public void DeleteSafetyRejectsEscapedLocalModelPathBeforeDeletingFile()
    {
        var allowedRoot = ModelDownloadManager.ModelsDirectoryPath;
        var sibling = allowedRoot + "-sibling";
        var escapedFile = Path.Combine(sibling, "model.gguf");

        Directory.CreateDirectory(sibling);
        File.WriteAllText(escapedFile, "not a model");

        try
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                PathGuard.EnsureContainedIn(escapedFile, allowedRoot);
                File.Delete(escapedFile);
            });

            Assert.That(File.Exists(escapedFile), Is.True);
        }
        finally
        {
            if (Directory.Exists(sibling))
                Directory.Delete(sibling, recursive: true);
        }
    }

    [Test]
    public void EnsureFileNameRejectsSeparatorsAndTraversal()
    {
        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentException>(() => PathGuard.EnsureFileName("..\\model.gguf"));
            Assert.Throws<ArgumentException>(() => PathGuard.EnsureFileName("../model.gguf"));
            Assert.Throws<ArgumentException>(() => PathGuard.EnsureFileName("model..gguf"));
            Assert.That(PathGuard.EnsureFileName("model.gguf"), Is.EqualTo("model.gguf"));
        });
    }

    private sealed class TemporaryDirectory : IDisposable
    {
        public string Path { get; } = System.IO.Path.Combine(
            System.IO.Path.GetTempPath(),
            "sharpclaw-providerintegrations-tests",
            Guid.NewGuid().ToString("N"));

        public TemporaryDirectory()
        {
            Directory.CreateDirectory(Path);
        }

        public void Dispose()
        {
            if (Directory.Exists(Path))
                Directory.Delete(Path, recursive: true);
        }
    }
}
