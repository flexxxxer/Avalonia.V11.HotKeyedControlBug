using System;
using System.Windows.Input;

using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.Styling;

namespace Avalonia.V11;

public class HotKeyedTextBox : TextBox, ICommandSource
{
    public static readonly StyledProperty<KeyGesture?> HotKeyProperty =
        HotKeyManager.HotKeyProperty.AddOwner<HotKeyedTextBox>();

    private KeyGesture? _hotkey;

    /// <summary>
    /// Gets or sets an <see cref="KeyGesture"/> associated with this control
    /// </summary>
    public KeyGesture? HotKey
    {
        get => GetValue(HotKeyProperty);
        set => SetValue(HotKeyProperty, value);
    }

    /// <inheritdoc/>
    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        if (_hotkey != null) // Control attached again, set Hotkey to create a hotkey manager for this control
        {
            this.SetValue(HotKeyProperty, _hotkey);
        }

        base.OnAttachedToLogicalTree(e);
    }

    /// <inheritdoc/>
    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        // This will cause the hotkey manager to dispose the observer and the reference to this control
        if (this.HotKey != null)
        {
            _hotkey = this.HotKey;
            this.SetValue(HotKeyProperty, null);
        }

        base.OnDetachedFromLogicalTree(e);
    }

    public void CanExecuteChanged(object sender, EventArgs e)
    {
    }

    protected override Type StyleKeyOverride => typeof(TextBox);

    public ICommand? Command => _command;

    public object? CommandParameter => null;

    CommunityToolkit.Mvvm.Input.RelayCommand _command;

    public HotKeyedTextBox()
    {
        _command = new CommunityToolkit.Mvvm.Input.RelayCommand(() => this.Focus());
    }
}
