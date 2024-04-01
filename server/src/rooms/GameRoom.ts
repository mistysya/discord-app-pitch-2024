import { Room, Client } from "colyseus";
import { GameRoomState, IState } from "./schema/GameRoomState";
import { Player, TPlayerOptions } from "./schema/Player";

export type PositionMessage = {
  x: number,
  y: number
}

export class GameRoom extends Room<GameRoomState> {

  onCreate (options: IState) {
    console.log("created!", options);
    this.setState(new GameRoomState(options));
  }

  onJoin (client: Client, options: TPlayerOptions) {
    console.log(client.sessionId, "joined!");

    this.state.createPlayer(client.sessionId, options);

    // Send welcome message to the client.
    client.send("welcomeMessage", "Welcome to Colyseus!");

    // Listen to position changes from the client.
    this.onMessage("position", (client, position: PositionMessage) => {
      this.state.updatePlayerPostion(client.sessionId, position.x, position.y);
    });
  }

  onLeave (client: Client, consented: boolean) {
    console.log(client.sessionId, "left!");
    this.state.removePlayer(client.sessionId);
  }

  onDispose() {
    console.log("room", this.roomId, "disposing...");
  }

}
