# Avalonia.V11.HotKeyedControlBug

[Avalonia v11 sample](https://github.com/flexxxxer/Avalonia.V11.HotKeyedControlBug/tree/master/Avalonia.V11) just not working, 
[Avalonia v0.10.21](https://github.com/flexxxxer/Avalonia.V11.HotKeyedControlBug/tree/master/Avalonia.V0.10) just working.

Reason: `Avalonia.Controls.HotkeyManager` in V11 [allows hotkey registration only on controls that implement IClickableControl](https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Controls/HotkeyManager.cs#L152), 
but in V0.10 hotkeys registration for controls that implement ICommandSource was allowed. Commit where this feature was removed - [here](https://github.com/AvaloniaUI/Avalonia/commit/d62381a0b218e063fcfb7b725130e5fead597428), 
PR with commit [here](https://github.com/AvaloniaUI/Avalonia/pull/7500).

Workaround for Avalonia V11: create hidden buttons with hotkeys and commands/event handlers which will do what you want.
