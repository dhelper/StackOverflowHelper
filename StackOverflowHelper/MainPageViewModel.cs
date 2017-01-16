using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using StackOverflowHelper.Annotations;

namespace StackOverflowHelper
{
    public enum State
    {
        Good,
        BadCouldBeWorse
    }

    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }


    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _userId = "22656";
        private string _theUser;
        private int _rep;
        private int _level1;
        private string _st1;
        private int _level2;
        private int _level3;
        private string _face;
        private State _trend;

        public ICommand LoadUserDetailsCommand
        {
            get { return new DelegateCommand(LoadUserDetails); }
        }

        private async void LoadUserDetails()
        {
            var theString = "https://api.stackexchange.com/2.1/users/" + UserId + "?site=stackoverflow";
            var information = WebRequest.CreateHttp(theString);

            WebResponse answer;

            // Get the response
            try
            {
                answer = await CallHome(information).ConfigureAwait(false);
            }
            catch (WebException exc)
            {
                if (exc.Response != null) // If valid response update screen status
                {
                    using (var errorResponse = (HttpWebResponse)exc.Response)
                    {
                        using (var sr = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = sr.ReadToEnd();
                            ST1 = error;
                        }
                    }
                }

                HandleError(exc);

                return;
            }


            var data = answer.GetResponseStream();
            var data2 = new GZipStream(data, CompressionMode.Decompress);
            var dataParseManager = new StreamReader(data2);
            try
            {
                theString = dataParseManager.ReadToEnd();


                var rootObject = JsonConvert.DeserializeObject<Rootobject>(theString);

                if (rootObject.quota_remaining < 10)
                {
                    ST1 = rootObject.quota_remaining + " calls left!";
                }

                // Load data to form 
                if (rootObject.items.Length > 0)
                {
                  
                    // Show user data
                    var item = rootObject.items[0];
                    if (Rep != item.reputation) // This means user has updated
                    {
                        // Level1-3 user data
                        Level1 = item.badge_counts.gold;
                        TheUser = item.display_name;
                        Level2 = item.badge_counts.silver;
                        Rep = item.reputation;
                        // TODO: connect to UI
                        Trend = Rep < item.reputation ? State.Good : State.BadCouldBeWorse;
                        Face = item.profile_image;
                        Level3 = item.badge_counts.bronze;
                    }
                }
            }
            finally
            {
                // Cleanup
                data.Dispose();
                data2.Dispose();
                dataParseManager.Dispose();
            }
        }

         public State Trend
        {
            get { return _trend; }
            set
            {
                if (value == _trend) return;
                _trend = value;
                OnPropertyChanged();
            }
        }

         public string UserId
        {
            get { return _userId; }
            set
            {
                if (value == _userId) return;
                _userId = value;
                OnPropertyChanged();
            }
        }

        public string TheUser
        {
            get { return _theUser; }
            set
            {
                if (value == _theUser) return;
                _theUser = value;
                OnPropertyChanged();
            }
        }

        public int Rep
        {
            get { return _rep; }
            set
            {
                if (value == _rep) return;
                _rep = value;
                OnPropertyChanged();
            }
        }

        public int Level1
        {
            get { return _level1; }
            set
            {
                if (value == _level1) return;
                _level1 = value;
                OnPropertyChanged();
            }
        }
        public int Level2
        {
            get { return _level2; }
            set
            {
                if (value == _level2) return;
                _level2 = value;
                OnPropertyChanged();
            }
        }
        public int Level3
        {
            get { return _level3; }
            set
            {
                if (value == _level3) return;
                _level3 = value;
                OnPropertyChanged();
            }
        }
        public async Task<WebResponse> CallHome(WebRequest request)
        {
            // Get a response from URL
            var t = Task.Factory.FromAsync((c, o) =>
                ((HttpWebRequest)o).BeginGetResponse(c, o), r => ((HttpWebRequest)r.AsyncState)
                    .EndGetResponse(r), request);

            return await t;
        }

        private void HandleError(WebException exc)
        {
            // specific handling of connection errors
            switch (exc.Status)
            {
                case WebExceptionStatus.CacheEntryNotFound:
                    break;
                case WebExceptionStatus.ConnectFailure:
                    break;
                case WebExceptionStatus.ConnectionClosed:
                    break;
                case WebExceptionStatus.KeepAliveFailure:
                    break;
                case WebExceptionStatus.MessageLengthLimitExceeded:
                    break;
                case WebExceptionStatus.NameResolutionFailure:
                    break;
                case WebExceptionStatus.Pending:
                    break;
                case WebExceptionStatus.PipelineFailure:
                    break;
                case WebExceptionStatus.ProtocolError:
                    break;
                case WebExceptionStatus.ProxyNameResolutionFailure:
                    break;
                case WebExceptionStatus.ReceiveFailure:
                    break;
                case WebExceptionStatus.RequestCanceled:
                    break;
                case WebExceptionStatus.RequestProhibitedByCachePolicy:
                    break;
                case WebExceptionStatus.RequestProhibitedByProxy:
                    break;
                case WebExceptionStatus.SecureChannelFailure:
                    break;
                case WebExceptionStatus.SendFailure:
                    break;
                case WebExceptionStatus.ServerProtocolViolation:
                    break;
                case WebExceptionStatus.Success:
                    ST1 = "Wait, WHAT?!";
                    break;
                case WebExceptionStatus.Timeout:
                    ST1 = "Timeout";
                    break;
                case WebExceptionStatus.TrustFailure:
                    break;
                case WebExceptionStatus.UnknownError:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // User notification property - shows application status
        public string ST1
        {
            get { return _st1; }
            set
            {
                if (value == _st1) return;
                _st1 = value;
                OnPropertyChanged();
            }
        }

        public string Face
        {
            get { return _face; }
            set
            {
                if (value == _face) return;
                _face = value;
                OnPropertyChanged();
            }
        }
    }
}
