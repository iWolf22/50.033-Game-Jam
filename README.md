# Rolling Tiles

**50.033 Midterm Game Jam: The Remix**

- **Team Members:** Joshua Dierickse
- **Game Title:** Rolling Tiles
- **Primary Song / Artist:** Never Gonna Give You Up (Rick Astley)
- **Remix Track / Artist:** Toxic (Britney Spears)

## 1. Theme Interpretation (25%)

- **Interpretation:** How did you interpret your primary song?

The first song I chose was Never Gonna Give You Up, which I found to have a very catchy rhythm. This inspired me to create a game that could leverage the song's rhythm and catchiness.

- **Implementation:** Which specific gameplay mechanics reflect this interpretation?

Initially, I started by creating a "press-and-hold" Piano Tiles-inspired game, where every word from the chorus would correspond to an individual tile. Due to difficulties implementing satisfying press-and-hold mechanics in a non-touchscreen environment, I transitioned to a tapping mechanic where each note was precisely aligned with the song's tempo.

## 2. Gameplay & Player Experience (30%)

- **Objective:** What must the player accomplish? What are the win/lose conditions?

The objective is to follow the rhythm of the song and play the falling notes at the correct time without making three mistakes. The player wins by completing the entire song with fewer than three mistakes and loses upon making three mistakes and exhausting all three lives.

- **Core Mechanics:** What are the main gameplay mechanics?

Tap the A/S/D/F keys in time with the falling tiles. If a tile is missed, the player loses a life. Similarly, pressing a key when no tile is present also results in losing a life.

- **Fun Factor:** What makes your game fun? What meaningful choices does the player make?

The game is designed to be both relaxing and competitive, depending on the player's style. Players can either enjoy the relaxing experience of a rhythm game while listening to some of their favourite songs or challenge themselves to complete the hardest levels flawlessly.

## 3. Remix Integration (20%)

- **Interpretation:** How did you interpret the remix track?

I found the second song, Toxic, to be much faster and more aggressive compared to Never Gonna Give You Up. With much of my game already implemented, I thought using it as a "boss track" would be quite fitting.

- **Implementation:** What mechanics were added or modified?

We were tasked with turning something beneficial in our games into something with an undesirable consequence. My resulting idea was that if the player flawlessly completes Never Gonna Give You Up without losing any lives, they are immediately challenged with Toxic, a much more difficult track.

- **Interaction:** How does the remix change or interact with the original gameplay?

Toxic increases the tempo from Never Gonna Give You Up's 113.63 beats per minute to 142.99 beats per minute, causing the tiles to fall significantly faster and making the gameplay more challenging. Upon flawlessly completing the first track, the player is presented with a "REMIX TIME" screen before transitioning to the new track.

- **Demonstration:** How can the grader observe the remix mechanic?

Play the first track flawlessly to enter remix mode. If needed, enable God Mode, which makes the player invincible and prevents their health from decreasing.

## 4. Technical Execution (15%)

- **How to Run:** Instructions to launch the game.

Either run the Windows executable at `\PianoTiles-Windows-x64\PianoTiles.exe` or open `Assets/Scenes/Main.unity` in the Unity Editor and press Play.

- **Controls:** List all player controls.

Tap the A/S/D/F keys to play the falling notes across the four lanes. There are no home or pause buttons by design.

- **Known Bugs / Limitations:** List all known issues, including steps to reproduce significant bugs. State "None" if there are no known issues.

I would have liked the boss fight against the Toxic track to be a bit more difficult, as the increase from 113.63 BPM to 142.99 BPM is only about 26%. Due to the need to maintain the song's tempo, I experimented with doubling the BPM but found it much too difficult. With limited alternatives, I opted for the original 142.99 BPM instead.

## 5. Game Design & Balance (10%)

- **Challenge:** What makes the game challenging?

The player must combine timing and coordination skills. They must not only hit each note at the correct time but also press the corresponding key, which I personally found quite difficult during playtesting.

- **Balance:** How do you prevent a single strategy from being overwhelmingly effective?

To balance the game, there are multiple difficulty levels for players of different skill levels. Different tracks feature varying speeds and tile sizes, creating variety, reducing repetitiveness, and limiting the effectiveness of any single strategy across all tracks.

- **Demonstration:** What specific gameplay situation should the grader try to evaluate the challenge and balance?

Try completing both tracks, then challenge yourself to finish them flawlessly without making any mistakes. Aim for a perfect high score.

## 6. Credits (Mandatory)

- Music Track 1: https://www.youtube.com/watch?v=dQw4w9WgXcQ&list=RDdQw4w9WgXcQ&start_radio=1
- Music Track 2: https://www.youtube.com/watch?v=LOZuxwVk7TU&list=RDLOZuxwVk7TU&start_radio=1

---

**Instructions:**

- All sections and fields are mandatory.
- Keep responses concise (1–2 sentences per field), except setup instructions, controls, bugs, and credits.
- Describe implemented features, not planned features.
- Ensure all described mechanics can be demonstrated in the submitted game.