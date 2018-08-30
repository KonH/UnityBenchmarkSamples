using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.PerformanceTesting;

public class SimpleBenchmarks {
	int          _size      = 1024 * 1204;
	int          _searchFor = 1024 * 1024 - 1;
	int[]        _array;
	List<int>    _list;
	HashSet<int> _set;

	[SetUp]
	public void Setup() {
		_array = new int[_size];
		_list = new List<int>();
		_set = new HashSet<int>();
		for (var i = 0; i < _size; i++) {
			_array[i] = i;
			_list.Add(i);
			_set.Add(i);
		}
	}
	
	[PerformanceTest]
	public void ArrayLookupTest() {
		Measure.Method(() => {
			foreach (var it in _array) {
				if (it == _searchFor) {
					return;
				}
			}
		}).WarmupCount(10).MeasurementCount(10).IterationsPerMeasurement(10).Run();
	}
	
	[PerformanceTest]
	public void ListLookupTest() {
		Measure.Method(() => {
			foreach (var it in _list) {
				if (it == _searchFor) {
					return;
				}
			}
		}).WarmupCount(10).MeasurementCount(10).IterationsPerMeasurement(10).Run();
	}
	
	[PerformanceTest]
	public void HashSetLookupTest() {
		Measure.Method(() => {
			if (_set.Contains(_searchFor)) {
				return;
			}
		}).WarmupCount(10).MeasurementCount(10).IterationsPerMeasurement(10).Run();
	}
}
