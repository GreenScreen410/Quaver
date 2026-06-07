using System;
using Quaver.Shared.Localization;
using Wobble.Graphics.Sprites.Text;
using Wobble.Managers;

namespace Quaver.Shared.Graphics
{
    /// <summary>
    ///     A <see cref="SpriteTextPlus"/> bound to a <see cref="TranslationKey"/>.
    ///
    ///     It sets its text from the current language on construction, and updates itself
    ///     automatically whenever the language changes (<see cref="Translator.Changed"/>),
    ///     so translated text refreshes live without a restart.
    /// </summary>
    public class LocalizedSpriteTextPlus : SpriteTextPlus
    {
        /// <summary>
        ///     The localization key this text is bound to.
        /// </summary>
        private readonly TranslationKey _key;

        /// <summary>
        ///     Optional transform applied to the localized string before display (e.g. uppercasing).
        /// </summary>
        private readonly Func<string, string> _transform;

        public LocalizedSpriteTextPlus(WobbleFontStore font, TranslationKey key, int size = 0,
            Func<string, string> transform = null, bool cache = true)
            : base(font, "", size, cache)
        {
            _key = key;
            _transform = transform;

            UpdateText();
            Translator.Changed += OnLanguageChanged;
        }

        private void OnLanguageChanged(object sender, EventArgs e) => UpdateText();

        private void UpdateText()
        {
            var value = Translator.Get(_key);
            Text = _transform != null ? _transform(value) : value;
        }

        public override void Destroy()
        {
            Translator.Changed -= OnLanguageChanged;
            base.Destroy();
        }
    }
}
