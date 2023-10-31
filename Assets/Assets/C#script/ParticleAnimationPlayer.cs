using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;

namespace Kek.Accelerator
{
    [ExecuteInEditMode]
    public class ParticleAnimationPlayer : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _data;
        [SerializeField]
        private ParticleBatchRenderer _renderer;
        [SerializeField]
        private float _frameRate = 60;
        [SerializeField]
        private int _frame;

        private byte[] _frameData;
        private List<Range> _frameRanges;
        private double _time;

        private int _currentFrame = -1;

        private void OnEnable()
        {
            if (_data)
            {
                LoadFrameData(_data.bytes);
            }

            if (_renderer)
            {
                _renderer.enabled = true;
            }

            _time = Time.timeAsDouble;

            if (Application.isPlaying)
            {
                _frame = 0;
            }
        }

        private void OnDisable()
        {
            if (_renderer)
            {
                _renderer.enabled = false;
            }

            _currentFrame = -1;
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (_frameRanges == null && _data)
            {
                LoadFrameData(_data.bytes);
            }
#endif
            if (_frameRanges.Count > 0 && _renderer)
            {
                if (Application.isPlaying)
                {
                    _time += Time.deltaTime;
                    _frame = Mathf.FloorToInt((float)(_time * _frameRate)) % _frameRanges.Count;
                }

                if (_currentFrame != _frame || !Application.isPlaying)
                {
                    _currentFrame = _frame;

                    ReadOnlySpan<Vector4> frame = GetFrame(_currentFrame);

                    _renderer.SetBatch(frame);
                }
            }
        }

        private void LoadFrameData(byte[] data)
        {
            if (_frameRanges != null)
            {
                _frameRanges.Clear();
            }
            else
            {
                _frameRanges = new List<Range>();
            }

            _frameData = data;

            Span<byte> buffer = _frameData;

            int frameCount = BinaryPrimitives.ReadInt32LittleEndian(buffer);

            buffer = buffer[4..];
            int current = 4;

            if (frameCount > 0)
            {
                _frameRanges.Capacity = frameCount;

                int vecSize = Marshal.SizeOf<Vector4>();
                while (buffer.Length > 0)
                {
                    int pc = BinaryPrimitives.ReadInt32LittleEndian(buffer);

                    buffer = buffer[4..];
                    current += 4;

                    int dataLength = pc * vecSize;
                    _frameRanges.Add(current..(current + dataLength));

                    buffer = buffer[dataLength..];
                    current += dataLength;
                }
            }
        }

        private ReadOnlySpan<Vector4> GetFrame(int frameIndex)
        {
            if (frameIndex < 0 || frameIndex >= _frameRanges.Count)
            {
                Debug.LogError("Frame index out of range");
                return ReadOnlySpan<Vector4>.Empty;
            }

            ReadOnlySpan<byte> frameData = _frameData;

            return MemoryMarshal.Cast<byte, Vector4>(frameData[_frameRanges[frameIndex]]);
        }
    }
}
