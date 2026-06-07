namespace Quaver.Shared.Localization
{
    /// <summary>
    ///     Keys for localized strings.
    ///
    ///     The name of each enum member MUST match a key inside the language files
    ///     (Quaver.Shared/Localization/{code}.txt), since <see cref="Translator.Get"/> uses
    ///     the stringified enum value as the lookup key.
    /// </summary>
    public enum TranslationKey
    {
        MainMenu_SinglePlayer,
        MainMenu_Multiplayer,
        MainMenu_Editor,
        MainMenu_DownloadSongs,
        MainMenu_SteamWorkshop,
        MainMenu_Options,
        MainMenu_QuitGame,
    }
}
