import React, { useState, useEffect } from "react";
import axios from "axios";
import ManagerSidebar from "../../components/manager/ManagerSidebar";
import Footer from "../../components/common/Footer";
import ScrollToTop from "../../components/common/ScrollToTop";
import "../../styles/pages/ManagerHomePage.scss";

const ManagerHomePage = () => {
  const [statistics, setStatistics] = useState({});
  const [bloodTypeRatio, setBloodTypeRatio] = useState({});

  useEffect(() => {
    // Gọi API cho thống kê
    axios
      .get("https://6840f16fd48516d1d359cdc9.mockapi.io/statistics")
      .then((response) => setStatistics(response.data))
      .catch((error) => console.error("Error fetching statistics:", error));

    // Gọi API cho tỷ lệ nhóm máu
    axios
      .get("https://6840f16fd48516d1d359cdc9.mockapi.io/blood-type-ratio")
      .then((response) => setBloodTypeRatio(response.data))
      .catch((error) =>
        console.error("Error fetching blood type ratio:", error)
      );
  }, []);

  // Dữ liệu tĩnh tạm thời cho các phần còn lại
  const requestsByMonth = {
    labels: ["Tháng 1", "Tháng 2", "Tháng 3"],
    datasets: [
      { label: "Nhóm A", data: [40, 80, 20], backgroundColor: "#FF4D4D" },
      { label: "Nhóm B", data: [60, 60, 90], backgroundColor: "#E5E5E5" },
      { label: "Nhóm O", data: [20, 30, 70], backgroundColor: "#2B6CB0" },
    ],
  };

  const usageOverTime = {
    labels: ["Tuần 1", "Tuần 2", "Tuần 3", "Tuần 4", "Tuần 5"],
    datasets: [
      {
        label: "Nhóm A",
        data: [150, 180, 220, 200, 190],
        borderColor: "#FF4D4D",
      },
      {
        label: "Nhóm B",
        data: [120, 140, 160, 130, 150],
        borderColor: "#E5E5E5",
      },
      {
        label: "Nhóm O",
        data: [100, 110, 130, 120, 140],
        borderColor: "#2B6CB0",
      },
    ],
  };

  const notifications = [
    {
      message:
        "Phạm Văn C vừa đăng ký hiến máu. Vui lòng kiểm tra thông tin chi tiết.",
      timestamp: "Hôm nay, 12 giờ chiều",
      isNew: true,
    },
    {
      message:
        "Phạm Văn C vừa đăng ký hiến máu. Vui lòng kiểm tra thông tin chi tiết.",
      timestamp: "Hôm qua, 15 giờ",
      isNew: false,
    },
  ];

  return (
    <>
      <ManagerSidebar />
      <div className="manager-home-page">
        <section className="dashboard-section">
          <h1 className="dashboard-title">THỐNG KÊ HỆ THỐNG</h1>
          <div className="dashboard-stats">
            <div className="stat-card">
              <h3>Số đơn vị máu</h3>
              <p className="stat-value">+{statistics.donors || 0}</p>
            </div>
            <div className="stat-card">
              <h3>Số người hiến</h3>
              <p className="stat-value">+{statistics.users || 0}</p>
            </div>
            <div className="stat-card">
              <h3>Số Yêu Cầu Máu</h3>
              <p className="stat-value">+{statistics.requests || 0}</p>
            </div>
            <div className="stat-card">
              <h3>Lượt Truy Cập</h3>
              <p className="stat-value">+{statistics.visits || 0}</p>
            </div>
          </div>
          <div className="dashboard-charts">
            <div className="chart-card pie-chart-card">
              <h3>Tỷ Lệ Nhóm Máu</h3>
              <div className="pie-chart">
                <div className="pie-chart-data">
                  <div
                    className="pie-segment"
                    style={{
                      background:
                        bloodTypeRatio.data && bloodTypeRatio.data.length
                          ? `conic-gradient(${bloodTypeRatio.colors[0]} 0% ${
                              (bloodTypeRatio.data[0] /
                                bloodTypeRatio.data.reduce(
                                  (a, b) => a + b,
                                  0
                                )) *
                              100
                            }%, ${bloodTypeRatio.colors[1]} ${
                              (bloodTypeRatio.data[0] /
                                bloodTypeRatio.data.reduce(
                                  (a, b) => a + b,
                                  0
                                )) *
                              100
                            }% 100%)`
                          : "gray",
                    }}
                  >
                    <span className="pie-value">
                      {bloodTypeRatio.data ? bloodTypeRatio.data[0] : "0"}
                    </span>
                  </div>
                </div>
                <div className="pie-legend">
                  {bloodTypeRatio.labels &&
                    bloodTypeRatio.labels.map((label, index) => (
                      <div key={index} className="legend-item">
                        <span
                          className="legend-color"
                          style={{
                            backgroundColor: bloodTypeRatio.colors
                              ? bloodTypeRatio.colors[index]
                              : "gray",
                          }}
                        ></span>
                        <span className="legend-label">{label || "N/A"}</span>
                      </div>
                    ))}
                </div>
              </div>
            </div>
            <div className="chart-card bar-chart-card">
              <h3>Yêu Cầu Theo Tháng</h3>
              <div className="bar-chart">
                <div className="bar-chart-container">
                  {requestsByMonth.labels.map((label, index) => (
                    <div key={index} className="bar-group">
                      {requestsByMonth.datasets.map((dataset, datasetIndex) => (
                        <div
                          key={datasetIndex}
                          className="bar"
                          style={{
                            height: `${dataset.data[index]}%`,
                            backgroundColor: dataset.backgroundColor,
                          }}
                        ></div>
                      ))}
                      <span className="bar-label">{label}</span>
                    </div>
                  ))}
                </div>
                <div className="bar-legend">
                  {requestsByMonth.datasets.map((dataset, index) => (
                    <div key={index} className="legend-item">
                      <span
                        className="legend-color"
                        style={{ backgroundColor: dataset.backgroundColor }}
                      ></span>
                      <span className="legend-label">{dataset.label}</span>
                    </div>
                  ))}
                </div>
              </div>
            </div>
          </div>
          <div className="dashboard-content">
            <div className="line-chart-card">
              <h3>Mức Sử Dụng Theo Thời Gian</h3>
              <div className="line-chart">
                <div className="line-chart-grid">
                  {[
                    300, 275, 250, 225, 200, 175, 150, 125, 100, 75, 50, 25, 0,
                  ].map((value) => (
                    <div key={value} className="grid-line">
                      <span className="grid-label">{value}</span>
                    </div>
                  ))}
                </div>
                <div className="line-data">
                  {usageOverTime.datasets.map((dataset, index) => (
                    <div key={index} className="line">
                      {dataset.data.map((value, i) => (
                        <div
                          key={i}
                          className="line-point"
                          style={{
                            bottom: `${(value / 300) * 100}%`,
                            left: `${(i / (dataset.data.length - 1)) * 100}%`,
                            backgroundColor: dataset.borderColor,
                          }}
                        ></div>
                      ))}
                      <svg className="line-path">
                        <polyline
                          points={dataset.data
                            .map(
                              (value, i) =>
                                `${(i / (dataset.data.length - 1)) * 100},${
                                  100 - (value / 300) * 100
                                }`
                            )
                            .join(" ")}
                          style={{
                            stroke: dataset.borderColor,
                            fill: "none",
                            strokeWidth: 2,
                          }}
                        />
                      </svg>
                    </div>
                  ))}
                </div>
                <div className="line-legend">
                  {usageOverTime.datasets.map((dataset, index) => (
                    <div key={index} className="legend-item">
                      <span
                        className="legend-color"
                        style={{ backgroundColor: dataset.borderColor }}
                      ></span>
                      <span className="legend-label">{dataset.label}</span>
                    </div>
                  ))}
                </div>
              </div>
            </div>
            <div className="notifications-card">
              <h3>THÔNG BÁO NỔI BẬT</h3>
              <ul>
                {notifications.map((notification, index) => (
                  <li key={index}>
                    <span className="notification-icon"></span>
                    <span className="notification-message">
                      {notification.message}
                    </span>
                    {notification.isNew && (
                      <span className="new-label">
                        {notification.timestamp}
                      </span>
                    )}
                  </li>
                ))}
              </ul>
            </div>
          </div>
        </section>
        <Footer />
      </div>
      <ScrollToTop />
    </>
  );
};

export default ManagerHomePage;
