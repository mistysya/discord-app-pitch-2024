import '../style/style.css';
import {discordSdk} from './discordSdk';
import type {AsyncReturnType} from 'type-fest';

window.global ||= window;

type Auth = AsyncReturnType<typeof discordSdk.commands.authenticate>;
let auth: Auth;

let channelName = 'NotAssigned';
let channelId = 'NotAssigned';
let userName = 'NotAssigned';
let userId = 'NotAssigned';

declare global {
  interface Window {
    GetChannelName:any;
    GetChannelId:any;
    GetUserName:any;
    GetUserId:any;
    DebugVariable: string;
  }
}

export function GetChannelName() {
  console.log("Get in TS:", channelName);
  return channelName;
}

export function GetChannelId() {
  console.log("Get in TS:", channelId);
  return channelId;
}

export function GetUserName() {
  console.log("Get in TS:", userName);
  return userName;
}

export function GetUserId() {
  console.log("Get in TS:", userId);
  return userId;
}

window.GetChannelName = GetChannelName;
window.GetChannelId = GetChannelId;
window.GetUserName = GetUserName;
window.GetUserId = GetUserId;
window.DebugVariable = "Hello World";
console.log("Got in TS:",window.DebugVariable);

// Once setupDiscordSdk is complete, we can assert that "auth" is initialized
setupDiscordSdk().then(() => {
  appendVoiceChannelName();
  appendUserId();
  // appendGuildAvatar();
});

async function setupDiscordSdk() {
  console.log('Setting up Discord SDK');
  await discordSdk.ready();

  // Authorize with Discord Client
  console.log('Authorizing with Discord Client');
  const {code} = await discordSdk.commands.authorize({
    client_id: import.meta.env.VITE_CLIENT_ID,
    response_type: 'code',
    state: '',
    prompt: 'none',
    // More info on scopes here: https://discord.com/developers/docs/topics/oauth2#shared-resources-oauth2-scopes
    scope: [
      // "applications.builds.upload",
      // "applications.builds.read",
      // "applications.store.update",
      // "applications.entitlements",
      // "bot",
      'identify',
      // "connections",
      // "email",
      // "gdm.join",
      'guilds',
      // "guilds.join",
      // "guilds.members.read",
      // "messages.read",
      // "relationships.read",
      // 'rpc.activities.write',
      // "rpc.notifications.read",
      // "rpc.voice.write",
      'rpc.voice.read',
      // "webhook.incoming",
    ],
  });

  // Retrieve an access_token from your activity's server
  console.log('Retrieving access token');
  const response = await fetch('/api/token', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      code,
    }),
  });
  const {access_token} = await response.json();

  // Authenticate with Discord client (using the access_token)
  console.log('Authenticating with Discord Client');
  auth = await discordSdk.commands.authenticate({
    access_token,
  });

  if (auth == null) {
    console.error('Authenticate command failed');
    throw new Error('Authenticate command failed');
  }
  console.log('Authenticated with Discord Client');
}

/**
 * This function fetches the current voice channel over RPC. It then creates a
 * text element that displays the voice channel's name
 */
async function appendVoiceChannelName() {
  const app = document.querySelector<HTMLDivElement>('#app')!;

  let activityChannelName = 'Unknown';

  // Requesting the channel in GDMs (when the guild ID is null) requires
  // the dm_channels.read scope which requires Discord approval.
  if (discordSdk.channelId != null && discordSdk.guildId != null) {
    // Over RPC collect info about the channel
    channelId = discordSdk.channelId;
    const channel = await discordSdk.commands.getChannel({channel_id: discordSdk.channelId});
    if (channel.name != null) {
      activityChannelName = channel.name;
      channelName = channel.name;
    }
  }

  // Update the UI with the name of the current voice channel
  const textTagString = `Activity Channel: "${activityChannelName}"`;
  const textTag = document.createElement('p');
  textTag.textContent = textTagString;
  app.appendChild(textTag);
  console.log(textTagString);
}

async function appendUserId() {
  // 1. From the HTTP API fetch a list of all of the user's info
  const userInfo: {id: string; username: string} = await fetch(`https://discord.com/api/users/@me`, {
    headers: {
      // NOTE: we're using the access_token provided by the "authenticate" command
      Authorization: `Bearer ${auth.access_token}`,
      'Content-Type': 'application/json',
    },
  }).then((reply) => reply.json());

  // 2. Append user id and username
  if (userInfo.id != null && userInfo.username != null) {
    userId = userInfo.id;
    userName = userInfo.username;
  }
}

/**
 * This function utilizes RPC and HTTP apis, in order show the current guild's avatar
 * Here are the steps:
 * 1. From RPC fetch the currently selected voice channel, which contains the voice channel's guild id
 * 2. From the HTTP API fetch a list of all of the user's guilds
 * 3. Find the current guild's info, including its "icon"
 * 4. Append to the UI an img tag with the related information
 */
/*
async function appendGuildAvatar() {
  const app = document.querySelector<HTMLDivElement>('#app')!;

  // 1. From the HTTP API fetch a list of all of the user's guilds
  const guilds: Array<{id: string; icon: string}> = await fetch(`https://discord.com/api/users/@me/guilds`, {
    headers: {
      // NOTE: we're using the access_token provided by the "authenticate" command
      Authorization: `Bearer ${auth.access_token}`,
      'Content-Type': 'application/json',
    },
  }).then((reply) => reply.json());

  // 2. Find the current guild's info, including it's "icon"
  const currentGuild = guilds.find((g) => g.id === discordSdk.guildId);

  // 3. Append to the UI an img tag with the related information
  if (currentGuild != null) {
    const guildImg = document.createElement('img');
    guildImg.setAttribute(
      'src',
      // More info on image formatting here: https://discord.com/developers/docs/reference#image-formatting
      `https://cdn.discordapp.com/icons/${currentGuild.id}/${currentGuild.icon}.webp?size=128`
    );
    guildImg.setAttribute('width', '128px');
    guildImg.setAttribute('height', '128px');
    guildImg.setAttribute('style', 'border-radius: 50%;');
    app.appendChild(guildImg);
  }
}
*/
