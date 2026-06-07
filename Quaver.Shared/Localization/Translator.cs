using System;
using System.Collections.Generic;
using Quaver.Shared.Config;
using Wobble;
using Wobble.IO;
using Wobble.Managers;

namespace Quaver.Shared.Localization
{
    /// <summary>
    ///     Thin wrapper around Wobble's <see cref="LocalizationManager"/> that adds:
    ///         * loading of the language files embedded in Quaver.Shared.dll
    ///         * a <see cref="Changed"/> event so on-screen text can refresh live, without a restart
    ///         * keeping the current language in sync with <see cref="ConfigManager.Language"/>
    /// </summary>
    public static class Translator
    {
        /// <summary>
        ///     The language code used when none is configured or a language file is missing.
        /// </summary>
        public const string DefaultLanguage = "en";

        /// <summary>
        ///     The languages the user can choose from. The code must match a language file
        ///     (Localization/{code}.txt); the name is what's shown in the UI.
        /// </summary>
        public static readonly IReadOnlyList<(string Code, string Name)> SupportedLanguages = new[]
        {
            ("en", "English"),
            ("ko", "한국어")
        };

        /// <summary>
        ///     Raised after the current language changes. UI text components subscribe to this
        ///     to update their displayed string immediately.
        /// </summary>
        public static event EventHandler Changed;

        private static bool _initialized;

        private static string ResourcePath(string code) => $"Localization/{code}.txt";

        /// <summary>
        ///     Initializes localization.
        ///
        ///     Must be called after <see cref="GameBase.Game"/> exists (so its resource store is
        ///     available) and after <see cref="ConfigManager.Initialize"/> has run.
        /// </summary>
        public static void Initialize()
        {
            if (_initialized)
                return;

            // Make the embedded language files reachable through GameBase.Game.Resources.
            GameBase.Game.Resources.AddStore(new DllResourceStore("Quaver.Shared.dll"));

            // English is always the fallback for any key missing in the current language.
            LocalizationManager.SetDefaultLanguageFile(ResourcePath(DefaultLanguage));

            // Apply the configured language, then keep it in sync with the config going forward.
            SetLanguage(ConfigManager.Language.Value);
            ConfigManager.Language.ValueChanged += (sender, e) => SetLanguage(e.Value);

            _initialized = true;
        }

        /// <summary>
        ///     Loads the given language code as the current language and notifies subscribers.
        ///     Falls back to English if the requested language file cannot be loaded.
        /// </summary>
        public static void SetLanguage(string code)
        {
            try
            {
                LocalizationManager.SetCurrentLanguage(ResourcePath(string.IsNullOrEmpty(code) ? DefaultLanguage : code));
            }
            catch (Exception)
            {
                LocalizationManager.SetCurrentLanguage(ResourcePath(DefaultLanguage));
            }

            Changed?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>
        ///     Gets the localized string for the given key in the current language.
        /// </summary>
        public static string Get(TranslationKey key, params object[] interpolated)
            => LocalizationManager.Get(key, interpolated);
    }
}
