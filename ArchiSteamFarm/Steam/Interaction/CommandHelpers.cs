// ----------------------------------------------------------------------------------------------
//     _                _      _  ____   _                           _____
//    / \    _ __  ___ | |__  (_)/ ___| | |_  ___   __ _  _ __ ___  |  ___|__ _  _ __  _ __ ___
//   / _ \  | '__|/ __|| '_ \ | |\___ \ | __|/ _ \ / _` || '_ ` _ \ | |_  / _` || '__|| '_ ` _ \
//  / ___ \ | |  | (__ | | | || | ___) || |_|  __/| (_| || | | | | ||  _|| (_| || |   | | | | | |
// /_/   \_\|_|   \___||_| |_||_||____/  \__|\___| \__,_||_| |_| |_||_|   \__,_||_|   |_| |_| |_|
// ----------------------------------------------------------------------------------------------
// |
// Copyright 2015-2026 Łukasz "JustArchi" Domeradzki
// Contact: JustArchi@JustArchi.net
// |
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// |
// http://www.apache.org/licenses/LICENSE-2.0
// |
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace ArchiSteamFarm.Steam.Interaction;

[PublicAPI]
public static class CommandHelpers {
	public static async Task<string?> Aggregate(EAccess access, string botNames, Func<Bot, EAccess, Task<string?>> executor, ulong steamID = 0) => throw new NotImplementedException();

	public static async Task<string?> Aggregate(EAccess access, string arg1Text, string botNames, Func<Bot, EAccess, string, Task<string?>> executor, ulong steamID = 0) => throw new NotImplementedException();

	public static async Task<string?> Aggregate<T1>(EAccess access, string arg1Text, string botNames, Func<Bot, EAccess, T1, Task<string?>> executor, ulong steamID = 0, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null)
		where T1 : IParsable<T1> =>
		throw new NotImplementedException();

	public static async Task<string?> Aggregate<T1, T2>(EAccess access, string arg1Text, string arg2Text, string botNames, Func<Bot, EAccess, T1, T2, Task<string?>> executor, ulong steamID = 0, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null, [CallerArgumentExpression(nameof(arg2Text))] string? arg2Name = null)
		where T1 : IParsable<T1>
		where T2 : IParsable<T2> =>
		throw new NotImplementedException();

	public static async Task<string?> Aggregate<T1, T2, T3>(EAccess access, string arg1Text, string arg2Text, string arg3Text, string botNames, Func<Bot, EAccess, T1, T2, T3, Task<string?>> executor, ulong steamID = 0, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null, [CallerArgumentExpression(nameof(arg2Text))] string? arg2Name = null, [CallerArgumentExpression(nameof(arg3Text))] string? arg3Name = null)
		where T1 : IParsable<T1>
		where T2 : IParsable<T2>
		where T3 : IParsable<T3> =>
		throw new NotImplementedException();

	public static async Task<string?> Aggregate<T1, T2, T3, T4>(EAccess access, string arg1Text, string arg2Text, string arg3Text, string arg4Text, string botNames, Func<Bot, EAccess, T1, T2, T3, T4, Task<string?>> executor, ulong steamID = 0, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null, [CallerArgumentExpression(nameof(arg2Text))] string? arg2Name = null, [CallerArgumentExpression(nameof(arg3Text))] string? arg3Name = null, [CallerArgumentExpression(nameof(arg4Text))] string? arg4Name = null)
		where T1 : IParsable<T1>
		where T2 : IParsable<T2>
		where T3 : IParsable<T3>
		where T4 : IParsable<T4> =>
		throw new NotImplementedException();

	public static async Task<string?> Aggregate<T1>(EAccess access, string arg1Text, string botNames, Func<Bot, EAccess, T1, Task<string?>> executor, Func<string, T1> parser, ulong steamID = 0) => throw new NotImplementedException();
	public static async Task<string?> Aggregate<T1, T2>(EAccess access, string arg1Text, string arg2Text, string botNames, Func<Bot, EAccess, T1, T2, Task<string?>> executor, Func<string, string, (T1, T2)> parser, ulong steamID = 0) => throw new NotImplementedException();
	public static async Task<string?> Aggregate<T1, T2, T3>(EAccess access, string arg1Text, string arg2Text, string arg3Text, string botNames, Func<Bot, EAccess, T1, T2, T3, Task<string?>> executor, Func<string, string, string, (T1, T2, T3)> parser, ulong steamID = 0) => throw new NotImplementedException();
	public static async Task<string?> Aggregate<T1, T2, T3, T4>(EAccess access, string arg1Text, string arg2Text, string arg3Text, string arg4Text, string botNames, Func<Bot, EAccess, T1, T2, T3, T4, Task<string?>> executor, Func<string, string, string, string, (T1, T2, T3, T4)> parser, ulong steamID = 0) => throw new NotImplementedException();

	public static async Task<string?> SingleBot<T1>(Bot bot, EAccess access, string arg1Text, Func<Bot, EAccess, T1, Task<string?>> executor, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null)
		where T1 : IParsable<T1> =>
		throw new NotImplementedException();

	public static async Task<string?> SingleBot<T1, T2>(Bot bot, EAccess access, string arg1Text, string arg2Text, Func<Bot, EAccess, T1, T2, Task<string?>> executor, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null, [CallerArgumentExpression(nameof(arg2Text))] string? arg2Name = null)
		where T1 : IParsable<T1>
		where T2 : IParsable<T2> =>
		throw new NotImplementedException();

	public static async Task<string?> SingleBot<T1, T2, T3>(Bot bot, EAccess access, string arg1Text, string arg2Text, string arg3Text, Func<Bot, EAccess, T1, T2, T3, Task<string?>> executor, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null, [CallerArgumentExpression(nameof(arg2Text))] string? arg2Name = null, [CallerArgumentExpression(nameof(arg3Text))] string? arg3Name = null)
		where T1 : IParsable<T1>
		where T2 : IParsable<T2>
		where T3 : IParsable<T3> =>
		throw new NotImplementedException();

	public static async Task<string?> SingleBot<T1, T2, T3, T4>(Bot bot, EAccess access, string arg1Text, string arg2Text, string arg3Text, string arg4Text, Func<Bot, EAccess, T1, T2, T3, T4, Task<string?>> executor, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null, [CallerArgumentExpression(nameof(arg2Text))] string? arg2Name = null, [CallerArgumentExpression(nameof(arg3Text))] string? arg3Name = null, [CallerArgumentExpression(nameof(arg4Text))] string? arg4Name = null)
		where T1 : IParsable<T1>
		where T2 : IParsable<T2>
		where T3 : IParsable<T3>
		where T4 : IParsable<T4> =>
		throw new NotImplementedException();

	public static async Task<string?> SingleBot<T1>(Bot bot, EAccess access, string arg1Text, Func<Bot, EAccess, T1, Task<string?>> executor, Func<string, T1> parser) => throw new NotImplementedException();
	public static async Task<string?> SingleBot<T1, T2>(Bot bot, EAccess access, string arg1Text, string arg2Text, Func<Bot, EAccess, T1, T2, Task<string?>> executor, Func<string, string, (T1, T2)> parser) => throw new NotImplementedException();
	public static async Task<string?> SingleBot<T1, T2, T3>(Bot bot, EAccess access, string arg1Text, string arg2Text, string arg3Text, Func<Bot, EAccess, T1, T2, T3, Task<string?>> executor, Func<string, string, string, (T1, T2, T3)> parser) => throw new NotImplementedException();
	public static async Task<string?> SingleBot<T1, T2, T3, T4>(Bot bot, EAccess access, string arg1Text, string arg2Text, string arg3Text, string arg4Text, Func<Bot, EAccess, T1, T2, T3, T4, Task<string?>> executor, Func<string, string, string, string, (T1, T2, T3, T4)> parser) => throw new NotImplementedException();

	internal static Func<Bot, EAccess, Task<string?>> Cmd(Func<EAccess, Task<string?>> func) => throw new NotImplementedException();
	internal static Func<Bot, EAccess, T1, Task<string?>> Cmd<T1>(Func<EAccess, T1, Task<string?>> func) => throw new NotImplementedException();
	internal static Func<Bot, EAccess, T1, T2, Task<string?>> Cmd<T1, T2>(Func<EAccess, T1, T2, Task<string?>> func) => throw new NotImplementedException();
	internal static Func<Bot, EAccess, T1, T2, T3, Task<string?>> Cmd<T1, T2, T3>(Func<EAccess, T1, T2, T3, Task<string?>> func) => throw new NotImplementedException();
	internal static Func<Bot, EAccess, T1, T2, T3, T4, Task<string?>> Cmd<T1, T2, T3, T4>(Func<EAccess, T1, T2, T3, T4, Task<string?>> func) => throw new NotImplementedException();
	internal static Func<Bot, EAccess, Task<string?>> Cmd(Func<EAccess, string?> func) => throw new NotImplementedException();
	internal static Func<Bot, EAccess, T1, Task<string?>> Cmd<T1>(Func<EAccess, T1, string?> func) => throw new NotImplementedException();
	internal static Func<Bot, EAccess, T1, T2, Task<string?>> Cmd<T1, T2>(Func<EAccess, T1, T2, string?> func) => throw new NotImplementedException();
	internal static Func<Bot, EAccess, T1, T2, T3, Task<string?>> Cmd<T1, T2, T3>(Func<EAccess, T1, T2, T3, string?> func) => throw new NotImplementedException();
	internal static Func<Bot, EAccess, T1, T2, T3, T4, Task<string?>> Cmd<T1, T2, T3, T4>(Func<EAccess, T1, T2, T3, T4, string?> func) => throw new NotImplementedException();
}
