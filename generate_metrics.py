import time
import requests
import json
import statistics
import threading

API_URL = "http://localhost:5242/api/copilot/ask"  # Update with actual port from launchSettings.json

def make_request(question, latencies):
    start_time = time.time()
    try:
        response = requests.post(API_URL, json={"Question": question}, timeout=30)
        if response.status_code == 200:
            end_time = time.time()
            latencies.append((end_time - start_time) * 1000)
        else:
            print(f"Request failed with status {response.status_code}")
    except Exception as e:
        print(f"Error: {e}")

def run_load_test():
    print("🚀 Starting DecideAI Backend Load Test & Metrics Generator...")
    questions = [
        "What are the Intel CPUs with more than 20 cores?",
        "What is the current market cap and PE ratio of AMD?",
        "Can you compare the core count of the Ryzen 9 7950X with the Core i9-14900K?",
        "What is the TDP of the EPYC 9654?",
        "Show me Intel processors with a base clock higher than 3.0 GHz."
    ]

    latencies = []
    threads = []
    
    # Send 50 concurrent requests
    for i in range(50):
        question = questions[i % len(questions)]
        t = threading.Thread(target=make_request, args=(question, latencies))
        threads.append(t)
        t.start()

    for t in threads:
        t.join()

    if latencies:
        avg_latency = statistics.mean(latencies)
        p95_latency = statistics.quantiles(latencies, n=20)[18]
        
        print("\n" + "="*50)
        print("📊 DECIDE AI - STAR RESUME METRICS GENERATOR")
        print("="*50)
        print(f"✅ Total Requests Processed: {len(latencies)} / 50")
        print(f"⏱️ Average API Latency: {avg_latency:.2f} ms")
        print(f"📈 95th Percentile Latency: {p95_latency:.2f} ms")
        print("\n💡 Suggested Resume Bullet Point:")
        print(f"• Architected a highly scalable .NET 8 AI Copilot utilizing Microsoft Semantic Kernel and MongoDB, serving concurrent enterprise data queries with an average response time of {avg_latency:.0f}ms and 95th percentile latency of {p95_latency:.0f}ms.")
        print("="*50)
    else:
        print("No requests completed successfully. Is the API running and accessible?")

if __name__ == "__main__":
    run_load_test()
