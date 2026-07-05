# SharpClaw.ProviderIntegrations

This repository contains SharpClaw provider integration packages that can be
consumed without referencing `SharpClaw.Application.*` projects or any local
application source shims. Shared provider helpers live under
`src/SharpClaw.Providers.Common`, and provider modules live under
`src/SharpClaw.Modules.Providers.*`.

A provider module may reference `SharpClaw.Contracts`, shared provider packages
from this repository, explicitly published SharpClaw hosting or SDK packages,
and ordinary framework or third-party packages. Extraction should stop when a
module needs an unpublished application-internal contract, because copying that
contract into this repository would hide a real package boundary problem.

The first extracted slice is the OpenAI-compatible provider family. It registers
OpenAI, DeepSeek, OpenRouter, Eden AI, Z.AI, Vercel AI Gateway, xAI, Groq,
Cerebras, Mistral, GitHub Copilot, Minimax, custom OpenAI-compatible, Google
Gemini OpenAI shim, and Google Vertex AI OpenAI shim providers.
