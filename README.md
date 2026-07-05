# SharpClaw.ProviderIntegrations

SharpClaw.ProviderIntegrations gives SharpClaw hosts provider modules for
remote and local AI services. Add `SharpClaw.Modules.Providers.OpenAICompatible`
to make OpenAI-style providers available at runtime, including OpenAI,
DeepSeek, OpenRouter, Eden AI, Z.AI, Vercel AI Gateway, xAI, Groq, Cerebras,
Mistral, GitHub Copilot, Minimax, custom OpenAI-compatible endpoints, Google
Gemini OpenAI shim, and Google Vertex AI OpenAI shim.

Add `SharpClaw.Modules.Providers.Anthropic` for Anthropic Messages API support,
`SharpClaw.Modules.Providers.Google` for native Google Gemini and Vertex AI
support, `SharpClaw.Modules.Providers.Ollama` for user-managed Ollama servers,
and `SharpClaw.Modules.Providers.LlamaSharp` for local GGUF inference through
LLamaSharp.

`SharpClaw.Providers.Common` supplies provider client, parameter, capability,
credential, and plugin helpers for modules that implement provider
integrations. `SharpClaw.Providers.LocalCommon` supplies Hugging Face GGUF URL
resolution, local model downloads, and local model path validation for provider
modules that manage local model files.
