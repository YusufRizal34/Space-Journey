using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    private static ViewManager _instance;

    private View _currentView;
    private readonly Stack<View> _history = new Stack<View>();

    [SerializeField] private View _startingView;
    [SerializeField] private View[] _views;

    private void Awake() => _instance = this;

    public static T GetView<T>() where T : View{
        for(int i = 0; i < _instance._views.Length; i++){
            if(_instance._views[i] is T tView){
                return tView;
            }
        }

        return null;
    }

    public static void Show<T>(bool remember = true) where T : View{
        for(int i = 0; i < _instance._views.Length; i++){
            if(_instance._views[i] is T){
                if(_instance._currentView != null){
                    if(remember){
                        _instance._history.Push(_instance._currentView);
                    }
                    _instance._views[i].Hide();
                }

                _instance._views[i].Show();
                _instance._currentView = _instance._views[i];
            }
        }
    }

    public static void Show(View view, bool remember = true){
        if(_instance._currentView != null){
            if(remember){
                _instance._history.Push(_instance._currentView);
            }

            _instance._currentView = view;
        }

        view.Show();
        _instance._currentView = view;
    }

    public static void ShowLast(){
        if(_instance._history.Count != 0){
            Show(_instance._history.Pop(), false);
        }
    }
}
