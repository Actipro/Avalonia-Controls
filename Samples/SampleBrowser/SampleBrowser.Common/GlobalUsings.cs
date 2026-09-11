global using Avalonia;
global using Avalonia.Controls;
global using Avalonia.Controls.Metadata;
global using Avalonia.Controls.Primitives;
global using Avalonia.Controls.Templates;
global using Avalonia.Data;
global using Avalonia.Input;
global using Avalonia.Interactivity;
global using Avalonia.Layout;
global using Avalonia.LogicalTree;
global using Avalonia.Media;
global using Avalonia.Media.Imaging;
global using Avalonia.Metadata;
global using Avalonia.Styling;
global using Avalonia.Threading;
global using Avalonia.VisualTree;
global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.ComponentModel;
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;
global using System.Globalization;
//global using System.IO; // Causes ambiguity with Avalonia.Controls.Shapes.Path
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using System.Windows.Input;

// Resolve ambiguity between 'System.Globalization.Calendar' and 'Avalonia.Controls.Calendar', since only Calendar controls are used by the sample
global using Calendar = Avalonia.Controls.Calendar;