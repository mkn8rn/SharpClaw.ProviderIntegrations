# SharpClaw.ProviderIntegrations

SharpClaw.ProviderIntegrations provides provider integration packages for
SharpClaw hosts and modules. The packages are intended to be consumed through
published package references, without referencing `SharpClaw.Application.*`
projects or local source shims.

`SharpClaw.Modules.Providers.OpenAICompatible` registers OpenAI, DeepSeek,
OpenRouter, Eden AI, Z.AI, Vercel AI Gateway, xAI, Groq, Cerebras, Mistral,
GitHub Copilot, Minimax, custom OpenAI-compatible, Google Gemini OpenAI shim,
and Google Vertex AI OpenAI shim providers.

`SharpClaw.Providers.Common` supplies the shared provider client, parameter,
capability, credential, and plugin helpers used by provider integration
packages. Consumers should reference the provider packages they need and let
NuGet resolve shared provider dependencies normally.
