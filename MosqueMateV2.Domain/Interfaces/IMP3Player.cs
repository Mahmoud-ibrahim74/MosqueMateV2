namespace MosqueMateV2.Domain.Interfaces
{
    /// <summary>
    /// Defines the contract for an MP3 player that can play, pause, and stop audio playback.
    /// </summary>
    public interface IMP3Player
    {
        /// <summary>
        /// Plays the MP3 file from the specified byte array.
        /// </summary>
        /// <param name="fileData">The byte array containing MP3 file data.</param>
        public void Play(byte[]? fileData = null);

        /// <summary>
        /// Pauses the current audio playback.
        /// </summary>
       public void Pause();

        /// <summary>
        /// Stops the current audio playback and resets the playback position.
        /// </summary>
        public void Stop();

        /// <summary>
        /// Gets or sets a value indicating whether the MP3 player is currently playing audio.
        /// </summary>
        public bool IsPlaying { get; set; }
    }

}
