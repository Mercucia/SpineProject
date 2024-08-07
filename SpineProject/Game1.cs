using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpineProject;

/**
 * The main class, which inherits from the MonoGame "Game" class and contains the game's core methods.
 * This project was initially based on the MonoGame "Getting Started" tutorial, which can be found at:
 * https://docs.monogame.net/articles/getting_started/
 * 
 * This project is being used as a sandbox for practising C# and getting to grips with the Spine
 * (Esoteric Software) runtimes.
 *
 * @author Katherine Town
 * @version 06/08/2024
 */

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D charSprite;
    private Vector2 charPosition;
    private float charSpeed;

    private int deadZone;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        charPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
        _graphics.PreferredBackBufferHeight / 2);
        charSpeed = 100f;
        deadZone = 4096;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        charSprite = Content.Load<Texture2D>("f001");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.Up))
        {
            charPosition.Y -= charSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (kstate.IsKeyDown(Keys.Down))
        {
            charPosition.Y += charSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (kstate.IsKeyDown(Keys.Left))
        {
            charPosition.X -= charSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (kstate.IsKeyDown(Keys.Right))
        {
            charPosition.X += charSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (Joystick.LastConnectedIndex == 0)
        {
            JoystickState jstate = Joystick.GetState(0);

            float updatedcharSpeed = charSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (jstate.Axes[1] < -deadZone)
            {
                charPosition.Y -= updatedcharSpeed;
            }
            else if (jstate.Axes[1] > deadZone)
            {
                charPosition.Y += updatedcharSpeed;
            }

            if (jstate.Axes[0] < -deadZone)
            {
                charPosition.X -= updatedcharSpeed;
            }
            else if (jstate.Axes[0] > deadZone)
            {
                charPosition.X += updatedcharSpeed;
            }
        }

        if (charPosition.X > _graphics.PreferredBackBufferWidth - charSprite.Width / 2)
        {
            charPosition.X = _graphics.PreferredBackBufferWidth - charSprite.Width / 2;
        }
        else if (charPosition.X < charSprite.Width / 2)
        {
            charPosition.X = charSprite.Width / 2;
        }

        if (charPosition.Y > _graphics.PreferredBackBufferHeight - charSprite.Height / 2)
        {
            charPosition.Y = _graphics.PreferredBackBufferHeight - charSprite.Height / 2;
        }
        else if (charPosition.Y < charSprite.Height / 2)
        {
            charPosition.Y = charSprite.Height / 2;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(
        charSprite,
        charPosition,
        null,
        Color.White,
        0f, new Vector2(charSprite.Width / 2, charSprite.Height / 2),
        Vector2.One,
        SpriteEffects.None,
        0f
        );
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
