using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniIOC.Framwork.Dependency;

namespace DependencyTest
{
    /// <summary>
    /// 用户播放媒体文件
    /// </summary>
    public class OperationMain
    {
        public IMediaFile _mtype;
        IPlayer _player;
        
        [Dependency("test")]
        public OperationMain(IPlayer player, IMediaFile mtype)
        {
            _player = player;
            _mtype = mtype;
        }

        public void PlayMedia()
        {
            _player.Play(_mtype);
        }
    }
    /// <summary>
    /// 播放器
    /// </summary>
    public interface IPlayer
    {
        void Play(IMediaFile file);
    }
    /// <summary>
    /// 默认播放器
    /// </summary>
    public class Player : IPlayer
    {
        public void Play(IMediaFile file)
        {
            Console.WriteLine(file.FilePath);
        }
    }
    /// <summary>
    /// 媒体文件
    /// </summary>
    public interface IMediaFile
    {
        string FilePath { get; set; }
    }
    /// <summary>
    /// 默认媒体文件
    /// </summary>
    public class MediaFile : IMediaFile
    {
        public string FilePath { get; set; }
    }
}
