// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 2.0.29
// 

using Colyseus.Schema;
using Action = System.Action;

public partial class Player : Schema {
	[Type(0, "string")]
	public string sessionId = default(string);

	[Type(1, "string")]
	public string userId = default(string);

	[Type(2, "string")]
	public string avatarUri = default(string);

	[Type(3, "string")]
	public string name = default(string);

	[Type(4, "number")]
	public float x = default(float);

	[Type(5, "number")]
	public float y = default(float);

	/*
	 * Support for individual property change callbacks below...
	 */

	protected event PropertyChangeHandler<string> __sessionIdChange;
	public Action OnSessionIdChange(PropertyChangeHandler<string> __handler, bool __immediate = true) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(this.sessionId));
		__sessionIdChange += __handler;
		if (__immediate && this.sessionId != default(string)) { __handler(this.sessionId, default(string)); }
		return () => {
			__callbacks.RemovePropertyCallback(nameof(sessionId));
			__sessionIdChange -= __handler;
		};
	}

	protected event PropertyChangeHandler<string> __userIdChange;
	public Action OnUserIdChange(PropertyChangeHandler<string> __handler, bool __immediate = true) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(this.userId));
		__userIdChange += __handler;
		if (__immediate && this.userId != default(string)) { __handler(this.userId, default(string)); }
		return () => {
			__callbacks.RemovePropertyCallback(nameof(userId));
			__userIdChange -= __handler;
		};
	}

	protected event PropertyChangeHandler<string> __avatarUriChange;
	public Action OnAvatarUriChange(PropertyChangeHandler<string> __handler, bool __immediate = true) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(this.avatarUri));
		__avatarUriChange += __handler;
		if (__immediate && this.avatarUri != default(string)) { __handler(this.avatarUri, default(string)); }
		return () => {
			__callbacks.RemovePropertyCallback(nameof(avatarUri));
			__avatarUriChange -= __handler;
		};
	}

	protected event PropertyChangeHandler<string> __nameChange;
	public Action OnNameChange(PropertyChangeHandler<string> __handler, bool __immediate = true) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(this.name));
		__nameChange += __handler;
		if (__immediate && this.name != default(string)) { __handler(this.name, default(string)); }
		return () => {
			__callbacks.RemovePropertyCallback(nameof(name));
			__nameChange -= __handler;
		};
	}

	protected event PropertyChangeHandler<float> __xChange;
	public Action OnXChange(PropertyChangeHandler<float> __handler, bool __immediate = true) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(this.x));
		__xChange += __handler;
		if (__immediate && this.x != default(float)) { __handler(this.x, default(float)); }
		return () => {
			__callbacks.RemovePropertyCallback(nameof(x));
			__xChange -= __handler;
		};
	}

	protected event PropertyChangeHandler<float> __yChange;
	public Action OnYChange(PropertyChangeHandler<float> __handler, bool __immediate = true) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(this.y));
		__yChange += __handler;
		if (__immediate && this.y != default(float)) { __handler(this.y, default(float)); }
		return () => {
			__callbacks.RemovePropertyCallback(nameof(y));
			__yChange -= __handler;
		};
	}

	protected override void TriggerFieldChange(DataChange change) {
		switch (change.Field) {
			case nameof(sessionId): __sessionIdChange?.Invoke((string) change.Value, (string) change.PreviousValue); break;
			case nameof(userId): __userIdChange?.Invoke((string) change.Value, (string) change.PreviousValue); break;
			case nameof(avatarUri): __avatarUriChange?.Invoke((string) change.Value, (string) change.PreviousValue); break;
			case nameof(name): __nameChange?.Invoke((string) change.Value, (string) change.PreviousValue); break;
			case nameof(x): __xChange?.Invoke((float) change.Value, (float) change.PreviousValue); break;
			case nameof(y): __yChange?.Invoke((float) change.Value, (float) change.PreviousValue); break;
			default: break;
		}
	}
}

