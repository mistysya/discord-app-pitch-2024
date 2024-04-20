import {Schema, type} from '@colyseus/schema';

export type TPlayerOptions = Pick<Player, 'sessionId' | 'userId' | 'name' | 'avatarUri' | 'x' | 'y'>;

export class Player extends Schema {
  @type('string')
  public sessionId: string;

  @type('string')
  public userId: string;

  @type('string')
  public avatarUri: string;

  @type('string')
  public name: string;

  @type("number") 
  public x: number = 0;

  @type("number") 
  public y: number = 0;

  // Init
  constructor({name, userId, avatarUri, sessionId}: TPlayerOptions) {
    super();
    this.userId = userId;
    this.avatarUri = avatarUri;
    this.name = name;
    this.sessionId = sessionId;
    this.x = 0;
    this.y = 0;
  }
}