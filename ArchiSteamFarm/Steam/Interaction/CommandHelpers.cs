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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ArchiSteamFarm.Core;
using ArchiSteamFarm.Localization;
using JetBrains.Annotations;

namespace ArchiSteamFarm.Steam.Interaction;

[PublicAPI]
public static class CommandHelpers {
	public static async Task<string?> Aggregate(EAccess access, string botNames, Func<Bot, EAccess, Task<string?>> executor, ulong steamID = 0) {
		ArgumentException.ThrowIfNullOrEmpty(botNames);
		ArgumentNullException.ThrowIfNull(executor);

		HashSet<Bot>? bots = Bot.GetBots(botNames);

		if ((bots == null) || (bots.Count == 0)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatBotNotFound(botNames)) : null;
		}

		IList<string?> results = await Utilities.InParallel(bots.Select(bot => executor(bot, Commands.GetProxyAccess(bot, access, steamID)))).ConfigureAwait(false);

		List<string> responses = [.. results.Where(static result => !string.IsNullOrEmpty(result)).Select(static result => result!)];

		return responses.Count > 0 ? string.Join(Environment.NewLine, responses) : null;
	}

	public static async Task<string?> Aggregate(EAccess access, string arg1Text, string botNames, Func<Bot, EAccess, string, Task<string?>> executor, ulong steamID = 0) {
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(botNames);
		ArgumentNullException.ThrowIfNull(executor);

		HashSet<Bot>? bots = Bot.GetBots(botNames);

		if ((bots == null) || (bots.Count == 0)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatBotNotFound(botNames)) : null;
		}

		IList<string?> results = await Utilities.InParallel(bots.Select(bot => executor(bot, Commands.GetProxyAccess(bot, access, steamID), arg1Text))).ConfigureAwait(false);

		List<string> responses = [.. results.Where(static result => !string.IsNullOrEmpty(result)).Select(static result => result!)];

		return responses.Count > 0 ? string.Join(Environment.NewLine, responses) : null;
	}

	public static async Task<string?> Aggregate<T1>(EAccess access, string arg1Text, string botNames, Func<Bot, EAccess, T1, Task<string?>> executor, ulong steamID = 0, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null)
		where T1 : IParsable<T1> {
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(botNames);
		ArgumentNullException.ThrowIfNull(executor);

		if (!T1.TryParse(arg1Text, CultureInfo.InvariantCulture, out T1? arg1)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg1Name)) : null;
		}

		HashSet<Bot>? bots = Bot.GetBots(botNames);

		if ((bots == null) || (bots.Count == 0)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatBotNotFound(botNames)) : null;
		}

		IList<string?> results = await Utilities.InParallel(bots.Select(bot => executor(bot, Commands.GetProxyAccess(bot, access, steamID), arg1))).ConfigureAwait(false);

		List<string> responses = [.. results.Where(static result => !string.IsNullOrEmpty(result)).Select(static result => result!)];

		return responses.Count > 0 ? string.Join(Environment.NewLine, responses) : null;
	}

	public static async Task<string?> Aggregate<T1, T2>(EAccess access, string arg1Text, string arg2Text, string botNames, Func<Bot, EAccess, T1, T2, Task<string?>> executor, ulong steamID = 0, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null, [CallerArgumentExpression(nameof(arg2Text))] string? arg2Name = null)
		where T1 : IParsable<T1>
		where T2 : IParsable<T2> {
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(arg2Text);
		ArgumentException.ThrowIfNullOrEmpty(botNames);
		ArgumentNullException.ThrowIfNull(executor);

		if (!T1.TryParse(arg1Text, CultureInfo.InvariantCulture, out T1? arg1)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg1Name)) : null;
		}

		if (!T2.TryParse(arg2Text, CultureInfo.InvariantCulture, out T2? arg2)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg2Name)) : null;
		}

		HashSet<Bot>? bots = Bot.GetBots(botNames);

		if ((bots == null) || (bots.Count == 0)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatBotNotFound(botNames)) : null;
		}

		IList<string?> results = await Utilities.InParallel(bots.Select(bot => executor(bot, Commands.GetProxyAccess(bot, access, steamID), arg1, arg2))).ConfigureAwait(false);

		List<string> responses = [.. results.Where(static result => !string.IsNullOrEmpty(result)).Select(static result => result!)];

		return responses.Count > 0 ? string.Join(Environment.NewLine, responses) : null;
	}

	public static async Task<string?> Aggregate<T1, T2, T3>(EAccess access, string arg1Text, string arg2Text, string arg3Text, string botNames, Func<Bot, EAccess, T1, T2, T3, Task<string?>> executor, ulong steamID = 0, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null, [CallerArgumentExpression(nameof(arg2Text))] string? arg2Name = null, [CallerArgumentExpression(nameof(arg3Text))] string? arg3Name = null)
		where T1 : IParsable<T1>
		where T2 : IParsable<T2>
		where T3 : IParsable<T3> {
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(arg2Text);
		ArgumentException.ThrowIfNullOrEmpty(arg3Text);
		ArgumentException.ThrowIfNullOrEmpty(botNames);
		ArgumentNullException.ThrowIfNull(executor);

		if (!T1.TryParse(arg1Text, CultureInfo.InvariantCulture, out T1? arg1)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg1Name)) : null;
		}

		if (!T2.TryParse(arg2Text, CultureInfo.InvariantCulture, out T2? arg2)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg2Name)) : null;
		}

		if (!T3.TryParse(arg3Text, CultureInfo.InvariantCulture, out T3? arg3)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg3Name)) : null;
		}

		HashSet<Bot>? bots = Bot.GetBots(botNames);

		if ((bots == null) || (bots.Count == 0)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatBotNotFound(botNames)) : null;
		}

		IList<string?> results = await Utilities.InParallel(bots.Select(bot => executor(bot, Commands.GetProxyAccess(bot, access, steamID), arg1, arg2, arg3))).ConfigureAwait(false);

		List<string> responses = [.. results.Where(static result => !string.IsNullOrEmpty(result)).Select(static result => result!)];

		return responses.Count > 0 ? string.Join(Environment.NewLine, responses) : null;
	}

	public static async Task<string?> Aggregate<T1, T2, T3, T4>(EAccess access, string arg1Text, string arg2Text, string arg3Text, string arg4Text, string botNames, Func<Bot, EAccess, T1, T2, T3, T4, Task<string?>> executor, ulong steamID = 0, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null, [CallerArgumentExpression(nameof(arg2Text))] string? arg2Name = null, [CallerArgumentExpression(nameof(arg3Text))] string? arg3Name = null, [CallerArgumentExpression(nameof(arg4Text))] string? arg4Name = null)
		where T1 : IParsable<T1>
		where T2 : IParsable<T2>
		where T3 : IParsable<T3>
		where T4 : IParsable<T4> {
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(arg2Text);
		ArgumentException.ThrowIfNullOrEmpty(arg3Text);
		ArgumentException.ThrowIfNullOrEmpty(arg4Text);
		ArgumentException.ThrowIfNullOrEmpty(botNames);
		ArgumentNullException.ThrowIfNull(executor);

		if (!T1.TryParse(arg1Text, CultureInfo.InvariantCulture, out T1? arg1)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg1Name)) : null;
		}

		if (!T2.TryParse(arg2Text, CultureInfo.InvariantCulture, out T2? arg2)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg2Name)) : null;
		}

		if (!T3.TryParse(arg3Text, CultureInfo.InvariantCulture, out T3? arg3)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg3Name)) : null;
		}

		if (!T4.TryParse(arg4Text, CultureInfo.InvariantCulture, out T4? arg4)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg4Name)) : null;
		}

		HashSet<Bot>? bots = Bot.GetBots(botNames);

		if ((bots == null) || (bots.Count == 0)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatBotNotFound(botNames)) : null;
		}

		IList<string?> results = await Utilities.InParallel(bots.Select(bot => executor(bot, Commands.GetProxyAccess(bot, access, steamID), arg1, arg2, arg3, arg4))).ConfigureAwait(false);

		List<string> responses = [.. results.Where(static result => !string.IsNullOrEmpty(result)).Select(static result => result!)];

		return responses.Count > 0 ? string.Join(Environment.NewLine, responses) : null;
	}

	public static async Task<string?> Aggregate<T1>(EAccess access, string arg1Text, string botNames, Func<Bot, EAccess, T1, Task<string?>> executor, Func<string, T1> parser, ulong steamID = 0) {
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(botNames);
		ArgumentNullException.ThrowIfNull(executor);
		ArgumentNullException.ThrowIfNull(parser);

		HashSet<Bot>? bots = Bot.GetBots(botNames);

		if ((bots == null) || (bots.Count == 0)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatBotNotFound(botNames)) : null;
		}

		T1 value;

		try {
			value = parser(arg1Text);
		} catch (Exception e) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(e.Message) : null;
		}

		IList<string?> results = await Utilities.InParallel(bots.Select(bot => executor(bot, Commands.GetProxyAccess(bot, access, steamID), value))).ConfigureAwait(false);

		List<string> responses = [.. results.Where(static result => !string.IsNullOrEmpty(result)).Select(static result => result!)];

		return responses.Count > 0 ? string.Join(Environment.NewLine, responses) : null;
	}

	public static async Task<string?> Aggregate<T1, T2>(EAccess access, string arg1Text, string arg2Text, string botNames, Func<Bot, EAccess, T1, T2, Task<string?>> executor, Func<string, string, (T1, T2)> parser, ulong steamID = 0) {
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(arg2Text);
		ArgumentException.ThrowIfNullOrEmpty(botNames);
		ArgumentNullException.ThrowIfNull(executor);
		ArgumentNullException.ThrowIfNull(parser);

		HashSet<Bot>? bots = Bot.GetBots(botNames);

		if ((bots == null) || (bots.Count == 0)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatBotNotFound(botNames)) : null;
		}

		T1 value1;
		T2 value2;

		try {
			(value1, value2) = parser(arg1Text, arg2Text);
		} catch (Exception e) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(e.Message) : null;
		}

		IList<string?> results = await Utilities
			.InParallel(bots.Select(bot => executor(bot, Commands.GetProxyAccess(bot, access, steamID), value1, value2)))
			.ConfigureAwait(false);

		List<string> responses = [.. results.Where(static result => !string.IsNullOrEmpty(result)).Select(static result => result!)];

		return responses.Count > 0 ? string.Join(Environment.NewLine, responses) : null;
	}

	public static async Task<string?> Aggregate<T1, T2, T3>(EAccess access, string arg1Text, string arg2Text, string arg3Text, string botNames, Func<Bot, EAccess, T1, T2, T3, Task<string?>> executor, Func<string, string, string, (T1, T2, T3)> parser, ulong steamID = 0) {
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(arg2Text);
		ArgumentException.ThrowIfNullOrEmpty(arg3Text);
		ArgumentException.ThrowIfNullOrEmpty(botNames);
		ArgumentNullException.ThrowIfNull(executor);
		ArgumentNullException.ThrowIfNull(parser);

		HashSet<Bot>? bots = Bot.GetBots(botNames);

		if ((bots == null) || (bots.Count == 0)) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(Strings.FormatBotNotFound(botNames)) : null;
		}

		T1 value1;
		T2 value2;
		T3 value3;

		try {
			(value1, value2, value3) = parser(arg1Text, arg2Text, arg3Text);
		} catch (Exception e) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(e.Message) : null;
		}

		IList<string?> results = await Utilities.InParallel(bots.Select(bot => executor(bot, Commands.GetProxyAccess(bot, access, steamID), value1, value2, value3))).ConfigureAwait(false);

		List<string> responses = [.. results.Where(static result => !string.IsNullOrEmpty(result)).Select(static result => result!)];

		return responses.Count > 0 ? string.Join(Environment.NewLine, responses) : null;
	}

	public static async Task<string?> Aggregate<T1, T2, T3, T4>(EAccess access, string arg1Text, string arg2Text, string arg3Text, string arg4Text, string botNames, Func<Bot, EAccess, T1, T2, T3, T4, Task<string?>> executor, Func<string, string, string, string, (T1, T2, T3, T4)> parser, ulong steamID = 0) {
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(arg2Text);
		ArgumentException.ThrowIfNullOrEmpty(arg3Text);
		ArgumentException.ThrowIfNullOrEmpty(arg4Text);
		ArgumentException.ThrowIfNullOrEmpty(botNames);
		ArgumentNullException.ThrowIfNull(executor);
		ArgumentNullException.ThrowIfNull(parser);

		HashSet<Bot>? bots = Bot.GetBots(botNames);

		if ((bots == null) || (bots.Count == 0)) {
			return access >= EAccess.Owner
				? Commands.FormatStaticResponse(Strings.FormatBotNotFound(botNames))
				: null;
		}

		T1 value1;
		T2 value2;
		T3 value3;
		T4 value4;

		try {
			(value1, value2, value3, value4) = parser(arg1Text, arg2Text, arg3Text, arg4Text);
		} catch (Exception e) {
			return access >= EAccess.Owner ? Commands.FormatStaticResponse(e.Message) : null;
		}

		IList<string?> results = await Utilities.InParallel(bots.Select(bot => executor(bot, Commands.GetProxyAccess(bot, access, steamID), value1, value2, value3, value4))).ConfigureAwait(false);

		List<string> responses = [.. results.Where(static result => !string.IsNullOrEmpty(result)).Select(static result => result!)];

		return responses.Count > 0 ? string.Join(Environment.NewLine, responses) : null;
	}

	public static async Task<string?> SingleBot<T1>(Bot bot, EAccess access, string arg1Text, Func<Bot, EAccess, T1, Task<string?>> executor, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null)
		where T1 : IParsable<T1> {
		ArgumentNullException.ThrowIfNull(bot);
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentNullException.ThrowIfNull(executor);

		if (!T1.TryParse(arg1Text, CultureInfo.InvariantCulture, out T1? arg1)) {
			return access >= EAccess.Master ? bot.Commands.FormatBotResponse(Strings.FormatErrorIsInvalid(arg1Name)) : null;
		}

		return await executor(bot, access, arg1).ConfigureAwait(false);
	}

	public static async Task<string?> SingleBot<T1, T2>(Bot bot, EAccess access, string arg1Text, string arg2Text, Func<Bot, EAccess, T1, T2, Task<string?>> executor, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null, [CallerArgumentExpression(nameof(arg2Text))] string? arg2Name = null)
		where T1 : IParsable<T1>
		where T2 : IParsable<T2> {
		ArgumentNullException.ThrowIfNull(bot);
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(arg2Text);
		ArgumentNullException.ThrowIfNull(executor);

		if (!T1.TryParse(arg1Text, CultureInfo.InvariantCulture, out T1? arg1)) {
			return access >= EAccess.Master ? bot.Commands.FormatBotResponse(Strings.FormatErrorIsInvalid(arg1Name)) : null;
		}

		if (!T2.TryParse(arg2Text, CultureInfo.InvariantCulture, out T2? arg2)) {
			return access >= EAccess.Master ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg2Name)) : null;
		}

		return await executor(bot, access, arg1, arg2).ConfigureAwait(false);
	}

	public static async Task<string?> SingleBot<T1, T2, T3>(Bot bot, EAccess access, string arg1Text, string arg2Text, string arg3Text, Func<Bot, EAccess, T1, T2, T3, Task<string?>> executor, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null, [CallerArgumentExpression(nameof(arg2Text))] string? arg2Name = null, [CallerArgumentExpression(nameof(arg3Text))] string? arg3Name = null)
		where T1 : IParsable<T1>
		where T2 : IParsable<T2>
		where T3 : IParsable<T3> {
		ArgumentNullException.ThrowIfNull(bot);
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(arg2Text);
		ArgumentException.ThrowIfNullOrEmpty(arg3Text);
		ArgumentNullException.ThrowIfNull(executor);

		if (!T1.TryParse(arg1Text, CultureInfo.InvariantCulture, out T1? arg1)) {
			return access >= EAccess.Master ? bot.Commands.FormatBotResponse(Strings.FormatErrorIsInvalid(arg1Name)) : null;
		}

		if (!T2.TryParse(arg2Text, CultureInfo.InvariantCulture, out T2? arg2)) {
			return access >= EAccess.Master ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg2Name)) : null;
		}

		if (!T3.TryParse(arg3Text, CultureInfo.InvariantCulture, out T3? arg3)) {
			return access >= EAccess.Master ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg3Name)) : null;
		}

		return await executor(bot, access, arg1, arg2, arg3).ConfigureAwait(false);
	}

	public static async Task<string?> SingleBot<T1, T2, T3, T4>(Bot bot, EAccess access, string arg1Text, string arg2Text, string arg3Text, string arg4Text, Func<Bot, EAccess, T1, T2, T3, T4, Task<string?>> executor, [CallerArgumentExpression(nameof(arg1Text))] string? arg1Name = null, [CallerArgumentExpression(nameof(arg2Text))] string? arg2Name = null, [CallerArgumentExpression(nameof(arg3Text))] string? arg3Name = null, [CallerArgumentExpression(nameof(arg4Text))] string? arg4Name = null)
		where T1 : IParsable<T1>
		where T2 : IParsable<T2>
		where T3 : IParsable<T3>
		where T4 : IParsable<T4> {
		ArgumentNullException.ThrowIfNull(bot);
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(arg2Text);
		ArgumentException.ThrowIfNullOrEmpty(arg3Text);
		ArgumentException.ThrowIfNullOrEmpty(arg4Text);
		ArgumentNullException.ThrowIfNull(executor);

		if (!T1.TryParse(arg1Text, CultureInfo.InvariantCulture, out T1? arg1)) {
			return access >= EAccess.Master ? bot.Commands.FormatBotResponse(Strings.FormatErrorIsInvalid(arg1Name)) : null;
		}

		if (!T2.TryParse(arg2Text, CultureInfo.InvariantCulture, out T2? arg2)) {
			return access >= EAccess.Master ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg2Name)) : null;
		}

		if (!T3.TryParse(arg3Text, CultureInfo.InvariantCulture, out T3? arg3)) {
			return access >= EAccess.Master ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg3Name)) : null;
		}

		if (!T4.TryParse(arg4Text, CultureInfo.InvariantCulture, out T4? arg4)) {
			return access >= EAccess.Master ? Commands.FormatStaticResponse(Strings.FormatErrorIsInvalid(arg4Name)) : null;
		}

		return await executor(bot, access, arg1, arg2, arg3, arg4).ConfigureAwait(false);
	}

	public static async Task<string?> SingleBot<T1>(Bot bot, EAccess access, string arg1Text, Func<Bot, EAccess, T1, Task<string?>> executor, Func<string, T1> parser) {
		ArgumentNullException.ThrowIfNull(bot);
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentNullException.ThrowIfNull(executor);
		ArgumentNullException.ThrowIfNull(parser);

		T1 value1;

		try {
			value1 = parser(arg1Text);
		} catch (Exception e) {
			return access >= EAccess.Master ? Commands.FormatStaticResponse(e.Message) : null;
		}

		return await executor(bot, access, value1).ConfigureAwait(false);
	}

	public static async Task<string?> SingleBot<T1, T2>(Bot bot, EAccess access, string arg1Text, string arg2Text, Func<Bot, EAccess, T1, T2, Task<string?>> executor, Func<string, string, (T1, T2)> parser) {
		ArgumentNullException.ThrowIfNull(bot);
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(arg2Text);
		ArgumentNullException.ThrowIfNull(executor);
		ArgumentNullException.ThrowIfNull(parser);

		T1 value1;
		T2 value2;

		try {
			(value1, value2) = parser(arg1Text, arg2Text);
		} catch (Exception e) {
			return access >= EAccess.Master ? Commands.FormatStaticResponse(e.Message) : null;
		}

		return await executor(bot, access, value1, value2).ConfigureAwait(false);
	}

	public static async Task<string?> SingleBot<T1, T2, T3>(Bot bot, EAccess access, string arg1Text, string arg2Text, string arg3Text, Func<Bot, EAccess, T1, T2, T3, Task<string?>> executor, Func<string, string, string, (T1, T2, T3)> parser) {
		ArgumentNullException.ThrowIfNull(bot);
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(arg2Text);
		ArgumentException.ThrowIfNullOrEmpty(arg3Text);
		ArgumentNullException.ThrowIfNull(executor);
		ArgumentNullException.ThrowIfNull(parser);

		T1 value1;
		T2 value2;
		T3 value3;

		try {
			(value1, value2, value3) = parser(arg1Text, arg2Text, arg3Text);
		} catch (Exception e) {
			return access >= EAccess.Master ? Commands.FormatStaticResponse(e.Message) : null;
		}

		return await executor(bot, access, value1, value2, value3).ConfigureAwait(false);
	}

	public static async Task<string?> SingleBot<T1, T2, T3, T4>(Bot bot, EAccess access, string arg1Text, string arg2Text, string arg3Text, string arg4Text, Func<Bot, EAccess, T1, T2, T3, T4, Task<string?>> executor, Func<string, string, string, string, (T1, T2, T3, T4)> parser) {
		ArgumentNullException.ThrowIfNull(bot);
		ArgumentException.ThrowIfNullOrEmpty(arg1Text);
		ArgumentException.ThrowIfNullOrEmpty(arg2Text);
		ArgumentException.ThrowIfNullOrEmpty(arg3Text);
		ArgumentException.ThrowIfNullOrEmpty(arg4Text);
		ArgumentNullException.ThrowIfNull(executor);
		ArgumentNullException.ThrowIfNull(parser);

		T1 value1;
		T2 value2;
		T3 value3;
		T4 value4;

		try {
			(value1, value2, value3, value4) = parser(arg1Text, arg2Text, arg3Text, arg4Text);
		} catch (Exception e) {
			return access >= EAccess.Master ? Commands.FormatStaticResponse(e.Message) : null;
		}

		return await executor(bot, access, value1, value2, value3, value4).ConfigureAwait(false);
	}

	//internal static Func<Bot, EAccess, Task<string?>> Convert(Func<Commands, Func<EAccess, Task<string?>>> func) => (bot, access) => func(bot.Commands)(access);
	internal static Func<Bot, EAccess, Task<string?>> Cmd(Func<EAccess, Task<string?>> func) => (bot, access) => func.Method.CreateDelegate<Func<Commands, EAccess, Task<string?>>>()(bot.Commands, access);
	internal static Func<Bot, EAccess, T1, Task<string?>> Cmd<T1>(Func<EAccess, T1, Task<string?>> func) => (bot, access, arg1) => func.Method.CreateDelegate<Func<Commands, EAccess, T1, Task<string?>>>()(bot.Commands, access, arg1);
	internal static Func<Bot, EAccess, T1, T2, Task<string?>> Cmd<T1, T2>(Func<EAccess, T1, T2, Task<string?>> func) => (bot, access, arg1, arg2) => func.Method.CreateDelegate<Func<Commands, EAccess, T1, T2, Task<string?>>>()(bot.Commands, access, arg1, arg2);
	internal static Func<Bot, EAccess, T1, T2, T3, Task<string?>> Cmd<T1, T2, T3>(Func<EAccess, T1, T2, T3, Task<string?>> func) => (bot, access, arg1, arg2, arg3) => func.Method.CreateDelegate<Func<Commands, EAccess, T1, T2, T3, Task<string?>>>()(bot.Commands, access, arg1, arg2, arg3);
	internal static Func<Bot, EAccess, T1, T2, T3, T4, Task<string?>> Cmd<T1, T2, T3, T4>(Func<EAccess, T1, T2, T3, T4, Task<string?>> func) => (bot, access, arg1, arg2, arg3, arg4) => func.Method.CreateDelegate<Func<Commands, EAccess, T1, T2, T3, T4, Task<string?>>>()(bot.Commands, access, arg1, arg2, arg3, arg4);
	internal static Func<Bot, EAccess, Task<string?>> Cmd(Func<EAccess, string?> func) => (bot, access) => Task.FromResult(func.Method.CreateDelegate<Func<Commands, EAccess, string?>>()(bot.Commands, access));
	internal static Func<Bot, EAccess, T1, Task<string?>> Cmd<T1>(Func<EAccess, T1, string?> func) => (bot, access, arg1) => Task.FromResult(func.Method.CreateDelegate<Func<Commands, EAccess, T1, string?>>()(bot.Commands, access, arg1));
	internal static Func<Bot, EAccess, T1, T2, Task<string?>> Cmd<T1, T2>(Func<EAccess, T1, T2, string?> func) => (bot, access, arg1, arg2) => Task.FromResult(func.Method.CreateDelegate<Func<Commands, EAccess, T1, T2, string?>>()(bot.Commands, access, arg1, arg2));
	internal static Func<Bot, EAccess, T1, T2, T3, Task<string?>> Cmd<T1, T2, T3>(Func<EAccess, T1, T2, T3, string?> func) => (bot, access, arg1, arg2, arg3) => Task.FromResult(func.Method.CreateDelegate<Func<Commands, EAccess, T1, T2, T3, string?>>()(bot.Commands, access, arg1, arg2, arg3));
	internal static Func<Bot, EAccess, T1, T2, T3, T4, Task<string?>> Cmd<T1, T2, T3, T4>(Func<EAccess, T1, T2, T3, T4, string?> func) => (bot, access, arg1, arg2, arg3, arg4) => Task.FromResult(func.Method.CreateDelegate<Func<Commands, EAccess, T1, T2, T3, T4, string?>>()(bot.Commands, access, arg1, arg2, arg3, arg4));
}
