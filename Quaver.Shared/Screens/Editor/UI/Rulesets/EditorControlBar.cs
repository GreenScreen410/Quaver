﻿using System;
using Microsoft.Xna.Framework;
using Quaver.Shared.Assets;
using Quaver.Shared.Audio;
using Quaver.Shared.Graphics;
using Quaver.Shared.Graphics.Notifications;
using Quaver.Shared.Helpers;
using Quaver.Shared.Screens.Menu.UI.Jukebox;
using Wobble.Graphics;
using Wobble.Graphics.Sprites;
using Wobble.Window;

namespace Quaver.Shared.Screens.Editor.UI.Rulesets
{
    public class EditorControlBar : Sprite
    {
        /// <summary>
        /// </summary>
        private JukeboxButton ButtonPlayTest { get; set; }

        /// <summary>
        /// </summary>
        private JukeboxButton ButtonPlaybackRate { get; set; }

        /// <summary>
        /// </summary>
        private JukeboxButton ButtonBeatSnap { get; set; }

        /// <summary>
        /// </summary>
        private JukeboxButton ButtonStopTrack { get; set; }

        /// <summary>
        /// </summary>
        private JukeboxButton ButtonPlayPauseTrack { get; set; }

        /// <summary>
        /// </summary>
        private EditorControlButton ButtonRestartTrack { get; set; }

        /// <summary>
        /// </summary>
        private SpriteTextBitmap TextAudioTime { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public EditorControlBar()
        {
            Size = new ScalableVector2(70, WindowManager.Height);
            Tint = Color.Black;
            Alpha = 0.70f;

            CreateAudioControlButtons();

            // Top Border Line
            // ReSharper disable once ObjectCreationAsStatement
            AddBorder(Color.White, 1);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            ButtonPlayPauseTrack.Image =  FontAwesome.Get(AudioEngine.Track.IsPlaying ? FontAwesomeIcon.fa_pause_symbol : FontAwesomeIcon.fa_play_button);
            base.Update(gameTime);
        }

        /// <summary>
        /// </summary>
        private void CreateTextAudioTime() => TextAudioTime = new SpriteTextBitmap(FontsBitmap.AllerRegular, "00:00.000")
        {
            Parent = this,
            Alignment = Alignment.MidLeft,
            X = 15,
            Y = 2,
            FontSize = 20
        };

        private void CreateAudioControlButtons()
        {
            ButtonPlayTest = new EditorControlButton(FontAwesome.Get(FontAwesomeIcon.fa_play_sign), "Test Play", 60)
            {
                Parent = this,
                Alignment = Alignment.BotCenter,
                Size = new ScalableVector2(30, 30),
                Y = -15
            };

            ButtonPlayTest.Clicked += (o, e) => NotificationManager.Show(NotificationLevel.Warning, "Play Testing is not implemented yet!");

            ButtonPlaybackRate = new EditorControlButton(FontAwesome.Get(FontAwesomeIcon.fa_time), "Change Playback Rate", 60)
            {
                Parent = this,
                Alignment = Alignment.BotCenter,
                Size = new ScalableVector2(30, 30),
                Y = ButtonPlayTest.Y - ButtonPlayTest.Height - 20
            };

            ButtonPlaybackRate.Clicked += (o, e) => NotificationManager.Show(NotificationLevel.Warning, "Not implemented yet");

            ButtonBeatSnap = new EditorControlButton(FontAwesome.Get(FontAwesomeIcon.fa_align_justify), "Change Beat Snap", 60)
            {
                Parent = this,
                Alignment = Alignment.BotCenter,
                Size = new ScalableVector2(30, 30),
                Y = ButtonPlaybackRate.Y - ButtonPlaybackRate.Height - 20
            };

            ButtonBeatSnap.Clicked += (o, e) => NotificationManager.Show(NotificationLevel.Warning, "Not implemented yet");

            ButtonStopTrack = new EditorControlButton(FontAwesome.Get(FontAwesomeIcon.fa_square_shape_shadow), "Stop Track", 60)
            {
                Parent = this,
                Alignment = Alignment.BotCenter,
                Size = new ScalableVector2(30, 30),
                Y = ButtonBeatSnap.Y - ButtonBeatSnap.Height - 20
            };

            ButtonStopTrack.Clicked += (o, e) => EditorScreen.StopTrack();

            ButtonPlayPauseTrack = new EditorControlButton(FontAwesome.Get(AudioEngine.Track.IsPlaying
                ? FontAwesomeIcon.fa_pause_symbol : FontAwesomeIcon.fa_play_button), "Play/Pause Track", 60)
            {
                Parent = this,
                Alignment = Alignment.BotCenter,
                Size = new ScalableVector2(30, 30),
                Y = ButtonStopTrack.Y - ButtonStopTrack.Height - 20
            };

            ButtonPlayPauseTrack.Clicked += (o, e) => EditorScreen.PlayPauseTrack();

            ButtonRestartTrack = new EditorControlButton(FontAwesome.Get(FontAwesomeIcon.fa_undo_arrow), "Restart Track", 60)
            {
                Parent = this,
                Alignment = Alignment.BotCenter,
                Size = new ScalableVector2(30, 30),
                Y = ButtonPlayPauseTrack.Y - ButtonPlayPauseTrack.Height - 20,
            };

            ButtonRestartTrack.Clicked += (o, e) => EditorScreen.RestartTrack();
        }
    }
}