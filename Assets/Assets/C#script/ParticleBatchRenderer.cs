using System;
using System.Collections.Generic;

using UnityEngine;

namespace Kek.Accelerator
{
    [ExecuteInEditMode]
    public class ParticleBatchRenderer : MonoBehaviour
    {
        private const int MaxBatchSize = 1000;

        [SerializeField]
        private Mesh _mesh;
        [SerializeField]
        private Material _material;
        [SerializeField]
        private float _particleSize = 0.1f;
        [SerializeField]
        private float _minValue = 1;
        [SerializeField]
        private float _maxValue = 40;
        [SerializeField]
        private Gradient _gradient;

        private List<Matrix4x4> _matrices = new(MaxBatchSize);
        private List<Vector4> _colors = new(MaxBatchSize);
        private Bounds _bounds;
        private MaterialPropertyBlock _matProps;
        private RenderParams _renderParams;

        public void SetBatch(ReadOnlySpan<Vector4> particleData)
        {
            _matrices.Clear();
            _colors.Clear();
            _bounds = default;

            Matrix4x4 parentMat = transform.localToWorldMatrix;
            Vector3 scale = Vector3.one * _particleSize;

            int count = Math.Min(particleData.Length, MaxBatchSize);
            for (int i = 0; i < count; i++)
            {
                Vector4 particle = particleData[i];

                Vector3 world = parentMat.MultiplyPoint(particle);

                _bounds.Encapsulate(world);

                _matrices.Add(Matrix4x4.TRS(world, Quaternion.identity, scale));

                float normalizedValue = Mathf.InverseLerp(_minValue, _maxValue, particle.w);
                Color color = _gradient?.Evaluate(normalizedValue) ?? Color.magenta;

                _colors.Add(color);
            }

            if (_matProps == null)
            {
                _matProps = new MaterialPropertyBlock();
            }
            else
            {
                _matProps.Clear();
            }

            _matProps.SetVectorArray("_Color", _colors);
        }

        private void Update()
        {
            if (_material && _mesh && _matrices.Count > 0)
            {
                if (_renderParams.material != _material)
                {
                    _renderParams = new RenderParams(_material);
                }

                _renderParams.material = _material;
                _renderParams.layer = gameObject.layer;
                _renderParams.worldBounds = _bounds;
                _renderParams.matProps = _matProps;

                Graphics.RenderMeshInstanced(_renderParams, _mesh, 0, _matrices);
            }
        }
    }
}
