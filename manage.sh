#!/bin/bash

# Colors for better UI
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
MAGENTA='\033[0;35m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color
BOLD='\033[1m'

# Docker compose files
DEV_COMPOSE="docker-compose.dev.yml"
PROD_COMPOSE="docker-compose.prod.yml"
DEV_PROJECT="baby-names-blazor-dev"
PROD_PROJECT="baby-names-blazor-prod"

# Function to print header
print_header() {
    clear
    echo -e "${CYAN}${BOLD}"
    echo "╔════════════════════════════════════════╗"
    echo "║   Docker Compose Manager - Baby Names  ║"
    echo "╚════════════════════════════════════════╝"
    echo -e "${NC}"
}

# Function to print menu
print_menu() {
    print_header
    echo -e "${BOLD}Select an option:${NC}\n"
    echo -e "${GREEN}  Development Environment:${NC}"
    echo -e "    ${CYAN}1)${NC} Start Dev"
    echo -e "    ${CYAN}2)${NC} Stop Dev"
    echo -e "    ${CYAN}3)${NC} Restart Dev"
    echo ""
    echo -e "${MAGENTA}  Production Environment:${NC}"
    echo -e "    ${CYAN}4)${NC} Start Prod"
    echo -e "    ${CYAN}5)${NC} Stop Prod"
    echo -e "    ${CYAN}6)${NC} Restart Prod"
    echo ""
    echo -e "${YELLOW}  Utilities:${NC}"
    echo -e "    ${CYAN}7)${NC} View Status (Dev)"
    echo -e "    ${CYAN}8)${NC} View Status (Prod)"
    echo -e "    ${CYAN}9)${NC} View Logs (Dev)"
    echo -e "    ${CYAN}10)${NC} View Logs (Prod)"
    echo ""
    echo -e "${RED}  0)${NC} Exit"
    echo ""
}

# Function to start dev
start_dev() {
    echo -e "${GREEN}${BOLD}Starting Development Environment...${NC}"
    docker compose -p $DEV_PROJECT -f $DEV_COMPOSE up -d --remove-orphans
    echo -e "${GREEN}✓ Development environment started${NC}"
}

# Function to stop dev
stop_dev() {
    echo -e "${YELLOW}${BOLD}Stopping Development Environment...${NC}"
    docker compose -p $DEV_PROJECT -f $DEV_COMPOSE down
    echo -e "${YELLOW}✓ Development environment stopped${NC}"
}

# Function to restart dev
restart_dev() {
    echo -e "${YELLOW}${BOLD}Restarting Development Environment...${NC}"
    docker compose -p $DEV_PROJECT -f $DEV_COMPOSE down
    docker compose -p $DEV_PROJECT -f $DEV_COMPOSE up -d --remove-orphans
    echo -e "${GREEN}✓ Development environment restarted${NC}"
}

# Function to start prod
start_prod() {
    echo -e "${GREEN}${BOLD}Starting Production Environment...${NC}"
    docker compose -p $PROD_PROJECT -f $PROD_COMPOSE up --build -d --remove-orphans
    echo -e "${GREEN}✓ Production environment started${NC}"
}

# Function to stop prod
stop_prod() {
    echo -e "${YELLOW}${BOLD}Stopping Production Environment...${NC}"
    docker compose -p $PROD_PROJECT -f $PROD_COMPOSE down
    echo -e "${YELLOW}✓ Production environment stopped${NC}"
}

# Function to restart prod
restart_prod() {
    echo -e "${YELLOW}${BOLD}Restarting Production Environment...${NC}"
    docker compose -p $PROD_PROJECT -f $PROD_COMPOSE down
    docker compose -p $PROD_PROJECT -f $PROD_COMPOSE up --build -d --remove-orphans
    echo -e "${GREEN}✓ Production environment restarted${NC}"
}

# Function to view dev status
status_dev() {
    echo -e "${BLUE}${BOLD}Development Environment Status:${NC}"
    docker compose -p $DEV_PROJECT -f $DEV_COMPOSE ps
}

# Function to view prod status
status_prod() {
    echo -e "${BLUE}${BOLD}Production Environment Status:${NC}"
    docker compose -p $PROD_PROJECT -f $PROD_COMPOSE ps
}

# Function to view dev logs
logs_dev() {
    echo -e "${BLUE}${BOLD}Development Environment Logs (Ctrl+C to exit):${NC}"
    docker compose -p $DEV_PROJECT -f $DEV_COMPOSE logs -f
}

# Function to view prod logs
logs_prod() {
    echo -e "${BLUE}${BOLD}Production Environment Logs (Ctrl+C to exit):${NC}"
    docker compose -p $PROD_PROJECT -f $PROD_COMPOSE logs -f
}

# Function to pause for user
pause() {
    echo ""
    read -p "Press [Enter] to continue..."
}

# One-shot menu: show options, execute the chosen action, then exit
print_menu
read -p "Enter your choice [0-10]: " choice
echo ""

case $choice in
    1)
        start_dev
        ;;
    2)
        stop_dev
        ;;
    3)
        restart_dev
        ;;
    4)
        start_prod
        ;;
    5)
        stop_prod
        ;;
    6)
        restart_prod
        ;;
    7)
        status_dev
        ;;
    8)
        status_prod
        ;;
    9)
        logs_dev
        ;;
    10)
        logs_prod
        ;;
    0)
        echo -e "${GREEN}${BOLD}Goodbye!${NC}"
        exit 0
        ;;
    *)
        echo -e "${RED}Invalid option.${NC}"
        exit 1
        ;;
esac

exit 0
